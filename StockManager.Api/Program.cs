using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using StockManager.Infrastructure.Persistence;
using StockManager.Application.Users.Commands;    // ensure these namespaces exist
using StockManager.Application.Products.Commands; // ensure these namespaces exist
using StockManager.Application.Interfaces;
using StockManager.Infrastructure.Repositories;
using StockManager.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ---------- CORS
var allowedOrigins = new[] {
    "http://localhost:5173",   // Vite / dev frontend (host)
    "http://localhost:5174",   // optional alt dev port
    "http://localhost:5000"    // if you want direct API UI calls
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ---------- Services
builder.Services.AddControllers();

// If you have FluentValidation validators for User/Product commands, register them here.
// If you do NOT have validators, comment/remove the next line to avoid compile errors.
// builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommand>(); // <-- enable only if validators exist

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockManager API", Version = "v1" });
});

// DbContext (prefer env var set by Docker: ConnectionStrings__DefaultConnection)
var connectionString = configuration.GetConnectionString("DefaultConnection") ??
    configuration["ConnectionStrings:DefaultConnection"] ??
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
    // default local fallback (use Host=db in Docker)
    "Host=db;Port=5432;Database=stockdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseNpgsql(connectionString));

// MediatR: register handlers from Application assembly (Users & Products)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateUserCommand).Assembly,
    typeof(CreateProductCommand).Assembly
));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();

// Enable Swagger in Development (or remove the check to enable in Prod too)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// IMPORTANT middleware ordering: Routing -> CORS -> Auth -> Endpoints
app.UseRouting();
app.UseCors("LocalDev");

// Only use HTTPS redirect in Development if you have certs; skip in container Prod
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

// Apply migrations + seeding (unchanged)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var cfg = services.GetRequiredService<IConfiguration>();
    var db = services.GetRequiredService<StockDbContext>();

    var maxAttempts = 12;
    var attempt = 0;
    var delayMs = 2000;
    var migrated = false;

    while (attempt < maxAttempts && !migrated)
    {
        try
        {
            attempt++;
            logger.LogInformation("Attempt {Attempt} to migrate database...", attempt);

            db.Database.Migrate();

            var adminEmail = cfg["ADMIN_EMAIL"] ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@local";
            var adminPassword = cfg["ADMIN_PASSWORD"] ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "P@ssw0rd";

            if (!db.Users.Any())
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(adminPassword);
                var admin = new User(adminEmail, hash);
                db.Users.Add(admin);
                db.SaveChanges();
                logger.LogInformation("Seeded admin user {Email}.", adminEmail);
            }
            else
            {
                logger.LogInformation("Users already exist in DB; skipping seed.");
            }

            migrated = true;
            logger.LogInformation("Migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database not ready yet (attempt {Attempt}/{MaxAttempts}). Waiting {Delay}ms then retrying...", attempt, maxAttempts, delayMs);
            await Task.Delay(delayMs);
        }
    }

    if (!migrated)
    {
        logger.LogError("Could not migrate database after {MaxAttempts} attempts — aborting startup.", maxAttempts);
    }
}

// Map controllers & run
app.MapControllers();
app.Run();
