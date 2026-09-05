# ECommerce REST API

A full-featured e-commerce backend API built with **ASP.NET Core Web API** 
following **Onion Architecture** for clean separation of concerns.

## 🚀 Features

- JWT Authentication with role-based authorization (Customer / Admin)
- Product catalog with categories and filtering
- Shopping cart management
- Checkout flow and order processing
- Admin dashboard for order tracking and management
- Generic Repository & Specification patterns for flexible, testable data access
- AutoMapper for clean object mapping
- Unit tests for core business logic validation
- Entity Framework Core with SQL Server

## 🏗️ Architecture

The project follows **Onion Architecture** with 4 layers:
ECommerceApi → Presentation Layer (Controllers, Middleware)
ECommerceApi.Application → Application Layer (Services, DTOs, Interfaces)
ECommerceApi.Infrastructure → Infrastructure Layer (EF Core, Repositories)
ECommerceApi.Domain → Domain Layer (Entities, Core Interfaces)


## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| Database | SQL Server + Entity Framework Core |
| Authentication | JWT Bearer Token |
| Mapping | AutoMapper |
| Testing | xUnit / NUnit |
| Patterns | Onion Architecture, Generic Repository, Specification Pattern |

## ⚙️ Getting Started

1. Clone the repository
```bash
   git clone https://github.com/tantawyahmed578-web/ECommerceApi.git
```
2. Update the connection string in `appsettings.json`
3. Run migrations:
```bash
   dotnet ef database update
```
4. Run the project:
```bash
   dotnet run
```
5. Open Swagger UI: `https://localhost:{port}/swagger`
