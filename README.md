# ECommerce REST API

A full-featured e-commerce backend API built with **ASP.NET Core Web API**
following **Onion Architecture** for clean separation of concerns.

## Features

- JWT Authentication with role-based authorization (Customer / Admin)
- Product catalog with categories and filtering
- Shopping cart management
- Checkout flow and order processing
- Admin dashboard for order tracking and management
- Generic Repository & Specification patterns for flexible, testable data access
- AutoMapper for clean object mapping
- Unit tests for core business logic validation
- Global exception handling middleware
- Entity Framework Core with SQL Server
- Swagger/OpenAPI documentation

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| Database | SQL Server + Entity Framework Core |
| Authentication | JWT Bearer Token |
| Mapping | AutoMapper |
| Testing | xUnit |
| Patterns | Onion Architecture, Generic Repository, Specification Pattern |

## Project Structure

```
ECommerceApi/
├── ECommerceApi/                  # Presentation Layer (Controllers, Middleware, Program.cs)
├── ECommerceApi.Application/      # Application Layer (Services, DTOs, Interfaces)
├── ECommerceApi.Domain/           # Domain Layer (Entities, Core Interfaces)
└── ECommerceApi.Infrastructure/   # Infrastructure Layer (EF Core, Repositories, DbContext)
```

## Architecture

The project follows **Onion Architecture** — all dependencies point inward toward the Domain layer:

- **Domain** — Entities and core interfaces. No external dependencies.
- **Application** — Business logic, DTOs, and service interfaces. Depends only on Domain.
- **Infrastructure** — EF Core implementation, Generic Repository, Unit of Work, and DbContext. Depends on Domain + Application.
- **API** — Controllers, middleware, and DI wiring. Depends on all layers.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/account/register` | Register a new user |
| POST | `/api/account/login` | Login and receive JWT token |
| GET | `/api/products` | Get all products (with filtering/sorting) |
| GET | `/api/products/{id}` | Get product by ID |
| GET | `/api/products/categories` | Get all categories |
| POST | `/api/basket` | Add item to cart |
| GET | `/api/basket` | Get current user's cart |
| DELETE | `/api/basket/{id}` | Remove item from cart |
| POST | `/api/orders` | Place a new order |
| GET | `/api/orders` | Get orders for current user |
| GET | `/api/orders/{id}` | Get order by ID |
| GET | `/api/admin/orders` | Get all orders (Admin only) |
| PATCH | `/api/admin/orders/{id}/status` | Update order status (Admin only) |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB is fine for local development)

### Setup

```bash
# Clone the repository
git clone https://github.com/tantawyahmed578-web/ECommerceApi.git
cd ECommerceApi

# Restore dependencies
dotnet restore

# Update the connection string in ECommerceApi/appsettings.json

# Run migrations
cd ECommerceApi
dotnet ef database update --project ../ECommerceApi.Infrastructure --startup-project .

# Run the project
dotnet run
```

Swagger UI will be available at `https://localhost:<port>/swagger`

## Architecture Notes

- **Domain** layer has zero external dependencies — only pure C# entities and interfaces.
- **Infrastructure** layer implements all persistence using EF Core with the Generic Repository and Specification patterns, keeping queries clean and reusable.
- **Application** layer holds all business logic and communicates with Infrastructure only through interfaces — fully testable with mocks.
- **API** layer wires everything via Dependency Injection and exposes REST endpoints with proper authorization policies.

This design means business rules are completely decoupled from EF Core and SQL Server — the data layer could be swapped without touching the Application or Domain layers.
