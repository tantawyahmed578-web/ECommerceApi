<div align="center">

# 🛍️ E-Commerce REST API

*A robust, production-ready backend API for e-commerce platforms, built with ASP.NET Core 9 following the principles of Onion Architecture.*

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-EF_Core-CC292B?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?style=for-the-badge&logo=redis&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Onion-00C7B7?style=for-the-badge)

</div>

---

## 📋 Overview

This project provides a comprehensive backend solution for e-commerce applications. By enforcing **Onion Architecture**, it ensures a clean separation of concerns, resulting in a highly testable, maintainable, and scalable system where the core business logic is completely isolated from external frameworks and infrastructure.

## ✨ Key Features

* **📦 Containerized Environment:** Fully orchestrated setup for the API, SQL Server 2022, and Redis using Docker Compose.
* **🛡️ Clean Architecture:** Strict adherence to Onion Architecture, utilizing Generic Repository and Specification patterns.
* **🔒 Secure Authentication:** JWT-based authentication with role-based authorization (Admin/Customer).
* **⚡ High Performance:** Caching integration via Redis to optimize read-heavy operations.
* **🚦 Centralized Error Handling:** Global exception handling via .NET `IExceptionHandler` for consistent API responses.
* **📝 Structured Logging:** Configured with Serilog for comprehensive tracking and debugging.
* **🛒 E-Commerce Workflows:** Full product catalog, category filtering, cart management, and order processing.

## 🛠️ Tech Stack

| Category | Technology |
|---|---|
| **Core Framework** | ASP.NET Core Web API (.NET 9) |
| **Data Access** | Entity Framework Core, SQL Server 2022 |
| **Caching** | Redis (Alpine) |
| **DevOps & Deployment** | Docker, Docker Compose |
| **Libraries** | AutoMapper, Serilog, Swagger/OpenAPI |
| **Testing** | xUnit |

## 🏗️ Architecture

The solution is divided into four strictly decoupled layers:

1. **`Domain Layer`**: Contains entities and domain exceptions. *Zero external dependencies.*
2. **`Application Layer`**: Contains business logic, DTOs, interfaces, and custom exceptions. *Depends only on Domain.*
3. **`Infrastructure Layer`**: Implements EF Core, DbContext, and Repositories. *Depends on Domain & Application.*
4. **`API Layer`**: The presentation layer containing Controllers, Middlewares, and DI wiring. *Depends on all layers.*

## 🚀 Getting Started

### Option A: Run with Docker (Recommended)
The fastest way to spin up the API with its dependencies (SQL Server & Redis).

```bash
# 1. Clone the repository
git clone [https://github.com/tantawyahmed578-web/ECommerceApi.git](https://github.com/tantawyahmed578-web/ECommerceApi.git)
cd ECommerceApi

# 2. Build and run the containers in detached mode
docker compose up -d --build

💻 Local Setup (Without Docker)
Ensure you have local instances of SQL Server and Redis running on your machine, then execute the following commands:

Bash
# 1. Restore dependencies
dotnet restore

# 2. Apply database migrations
cd ECommerceApi
dotnet ef database update --project ../ECommerceApi.Infrastructure --startup-project .

# 3. Run the application
dotnet run
🌐 API Reference
Account & Authentication

POST /api/Auth/register - Register a new user account

POST /api/Auth/login - Authenticate and retrieve JWT token

Product Catalog

GET /api/Product - Retrieve all products (Supports pagination, filtering, and sorting)

GET /api/Product/{id} - Retrieve product details

GET /api/Categories - Retrieve available categories

Basket / Cart

POST /api/basket - Add or update an item in the cart

GET /api/basket - Retrieve the current user's cart

DELETE /api/basket/{id} - Remove an item from the cart

Orders & Checkout

POST /api/orders - Place a new order

GET /api/orders - Retrieve order history for the current user

GET /api/orders/{id} - Retrieve specific order details

Admin Operations (Requires Admin Role)

GET /api/admin/orders - View all system orders

PATCH /api/admin/orders/{id}/status - Update the fulfillment status of an order
