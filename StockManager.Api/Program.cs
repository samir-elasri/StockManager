using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using StockManager.Infrastructure.Persistence;
using StockManager.Application.Categories.Commands;
using StockManager.Application.Interfaces;
using StockManager.Infrastructure.Repositories;
using StockManager.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// --- Configuration
var configuration = builder.Configuration;

// --- Add services
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryCommand>());

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockManager API", Version = "v1" });
});

// DbContext (Postgres)
var connectionString = configuration.GetConnectionString("DefaultConnection") ??
    configuration["ConnectionStrings:DefaultConnection"] ??
    // fallback to env var style
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
    "Host=localhost;Port=5432;Database=stockdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseNpgsql(connectionString));

// MediatR (register from Application assembly)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    typeof(CreateCategoryCommand).Assembly
));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var app = builder.Build();

// Enable Swagger in all environments (for development containers)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// --- Apply migrations + seeding with retry logic
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var cfg = services.GetRequiredService<IConfiguration>();

    // Retrieve DB context
    var db = services.GetRequiredService<StockDbContext>();

    // Retry loop: attempt to connect & migrate several times (useful when db container still starting)
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

            // ensure DB provider can be connected
            db.Database.Migrate(); // this applies migrations

            // Seed default user (if none exists)
            var adminEmail = cfg["ADMIN_EMAIL"] ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@local";
            var adminPassword = cfg["ADMIN_PASSWORD"] ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "P@ssw0rd";

            // check if user exists (simple check)
            if (!db.Users.Any())
            {
                // make password hash using BCrypt (ensure BCrypt.Net-Next package installed)
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
        // You can decide to throw here to stop the app if desired:
        // throw new Exception("DB migration failed after retries", lastException);
    }
}

// Map controllers & run
app.MapControllers();
app.Run();
