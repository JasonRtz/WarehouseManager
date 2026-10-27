# WarehouseManager

WarehouseManager is a warehouse inventory management REST API built with ASP.NET Core, Entity Framework Core, and PostgreSQL.

The project is currently under development and aims to evolve into a full-stack inventory management application.

## Technologies

- C# / .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker Compose

## Current Features

- Create, retrieve, update, and delete products (CRUD).
- Persistent product storage using PostgreSQL.
- Product validation using Data Annotations.
- Case-insensitive unique SKU enforcement.
- HTTP error handling, including `404 Not Found` and `409 Conflict`.
- Database schema management using EF Core migrations.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/products` | Retrieve all products |
| GET | `/api/products/{id}` | Retrieve a product by ID |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |

## Getting Started

### Requirements

- .NET 10 SDK
- Docker with Docker Compose

### Database Setup

1. Create a `.env` file in the repository root containing:

   ```env
   POSTGRES_PASSWORD=your_password
   ```

2. Start PostgreSQL:

   ```bash
   docker compose up -d
   ```

3. Configure the `ConnectionStrings:WarehouseDatabase` connection string using .NET User Secrets, with the same password.

4. Apply the database migrations:

   ```bash
   dotnet ef database update --project WarehouseManager.Api
   ```

5. Run the API:

   ```bash
   dotnet run --project WarehouseManager.Api
   ```

## Project Status

**Work in progress.**

Planned improvements include DTOs, additional inventory management features, automated testing, and a web-based frontend.

