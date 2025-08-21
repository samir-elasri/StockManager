
# StockManager

A minimal Product Management app  
Backend: .NET 9 Web API (Users + Products) | Database: PostgreSQL | Frontend: Vue 3 + TypeScript (Vite)  
This README explains how to run the system (Docker & local), where to find API docs, how to run tests, and troubleshooting tips.

---

## Quick links (when running via Docker on your machine)
- **Swagger (API docs)**: `http://localhost:5000/swagger`  
- **API base**: `http://localhost:5000/api`  
- **Frontend (Vite dev)**: `http://localhost:5173` *(if you run client locally)*  
- **pgAdmin (DB GUI)**: `http://localhost:5050`

**Seeded admin (created automatically on first startup):**
- Email: `admin@local`  
- Password: `P@ssw0rd` (or value of `ADMIN_PASSWORD` env var)

---

## Requirements
- .NET 9 SDK (for local build & `dotnet ef`)
- Node 18+ (for frontend if running locally)
- Docker Desktop (for Docker Compose)
- Optional: `dotnet-ef` tool (`dotnet tool install --global dotnet-ef`)

---

## Run everything with Docker Compose (recommended)

From repo root:

1. Build & start containers:
```powershell
docker compose up -d --build
```

2. Tail API logs (watch migrations + seeding):
```powershell
docker compose logs -f api
```

3. Confirm containers:
```powershell
docker compose ps
```

4. Open in browser:
- Swagger: `http://localhost:5000/swagger`  
- pgAdmin: `http://localhost:5050` (login: `admin@admin.com` / `admin`)  

---

## Run frontend locally (Vite)

If you prefer local frontend dev (hot reload):

```powershell
cd client/vue
npm install
# ensure .env has VITE_API_URL=http://localhost:5000/api (default)
npm run dev
```

Open: `http://localhost:5173`

> If you want the frontend built and served from a container, see the "Serve built frontend from container" note below.

---

## Run API locally (without Docker)

1. Ensure Postgres is running locally and update connection string (in `appsettings.Development.json` or env):
```powershell
$env:ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=stockdb;Username=postgres;Password=postgres"
```

2. Apply EF migrations:
```powershell
dotnet ef database update --project StockManager.Infrastructure --startup-project StockManager.Api
```

3. Run API:
```powershell
cd StockManager.Api
dotnet run
```

Swagger will be available at the URL printed by the console.

---

## EF Migrations notes

- The API calls `db.Database.Migrate()` on startup (so Dockerized API will try to apply migrations automatically).
- To create a new migration:
```powershell
dotnet ef migrations add <Name> --project StockManager.Infrastructure --startup-project StockManager.Api
dotnet ef database update --project StockManager.Infrastructure --startup-project StockManager.Api
```
- If you see `PendingModelChangesWarning`, create a migration that captures your model changes, or recreate migrations for a fresh DB.

---

## Database CLI (`psql`) via Docker

Open psql in the db container:
```powershell
docker compose exec db psql -U postgres -d stockdb
# inside psql:
\dt
SELECT * FROM "Users" LIMIT 10;
SELECT * FROM "Products" LIMIT 10;
\q
```

Single-line examples:
```powershell
docker compose exec db psql -U postgres -d stockdb -c "\dt"
docker compose exec db psql -U postgres -d stockdb -c "SELECT * FROM \"Users\" LIMIT 5;"
```

---

## Tests (xUnit) — run from repo root

Run all tests:
```powershell
dotnet test
```

Run only the test project:
```powershell
dotnet test .\StockManager.Tests\StockManager.Tests.csproj
```

Run a single test by filter:
```powershell
dotnet test --filter "DisplayName~Handle_Should_Add_Product"
```

---

## Quick API examples (curl)

Register user:
```bash
curl -i -X POST "http://localhost:5000/api/Auth/register" \
  -H "Content-Type: application/json" \
  -d '{"email":"me@local","passwordHash":"P@ssw0rd"}'
```

Create product:
```bash
curl -i -X POST "http://localhost:5000/api/Products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Widget","price":9.99,"userId":"<user-guid>"}'
```

Get products for user:
```bash
curl -i "http://localhost:5000/api/Products/<user-guid>"
```

---

## Environment variables (important)

When running via Docker Compose, the `api` service should receive:
- `ConnectionStrings__DefaultConnection` — e.g. `Host=db;Port=5432;Database=stockdb;Username=postgres;Password=postgres`
- `ADMIN_EMAIL`, `ADMIN_PASSWORD` — seeded admin credentials
- `ASPNETCORE_ENVIRONMENT` — set `Development` to enable Swagger in the container

You can override env values when running locally or via `docker compose run`.

---

## Troubleshooting

- **404 on `/swagger`**: Confirm API started successfully (`docker compose logs api`). If `ASPNETCORE_ENVIRONMENT` is not `Development`, Swagger may be disabled.
- **Cannot connect to DB**: Ensure `db` is running and `ConnectionStrings__DefaultConnection` points to `Host=db` when running in Docker.
- **`PendingModelChangesWarning`**: create a migration for the model changes or recreate migrations if you want a fresh DB.
- **CORS errors**: ensure `Program.cs` includes a CORS policy allowing your frontend origin and that middleware ordering is `UseRouting()` → `UseCors()` → `UseAuthorization()`.

---

## Project structure (short)
```
/StockManager.sln
  /StockManager.Api
  /StockManager.Application
  /StockManager.Domain
  /StockManager.Infrastructure
  /StockManager.Tests
  /client/vue
  docker-compose.yml
```

---