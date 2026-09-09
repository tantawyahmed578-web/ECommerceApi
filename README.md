# ECommerce REST API

A full-featured, production-ready e-commerce backend API built with ASP.NET Core Web API (.NET 8) following the Onion Architecture for clean separation of concerns.

## Features

* **Containerized Environment**: Fully orchestrated setup for the API, SQL Server, and Redis using Docker Compose.
* **Global Exception Handling**: Centralized error management using the modern `.NET 8` `IExceptionHandler` middleware.
* **Structured Logging**: Integrated Serilog for detailed, structured application logs.
* **Caching**: Redis integration for performance optimization.
* **Authentication & Authorization**: JWT Bearer Tokens with role-based access control (Customer / Admin).
* **Product Catalog**: Category management, filtering, and sorting.
* **Shopping Cart & Checkout**: Basket management and comprehensive order processing flow.
* **Admin Dashboard**: Secure endpoints for order tracking and status management.
* **Clean Architecture**: Generic Repository & Specification patterns for flexible, testable data access.
* **Object Mapping**: AutoMapper for clean DTO-to-Entity conversions.
* **Swagger/OpenAPI**: Interactive API documentation.

## Tech Stack

| Component | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core Web API (.NET 8) |
| **Database** | SQL Server + Entity Framework Core |
| **Caching** | Redis |
| **Containerization**| Docker & Docker Compose |
| **Logging** | Serilog |
| **Authentication**| JWT Bearer Token |
| **Mapping** | AutoMapper |
| **Testing** | xUnit |
| **Patterns** | Onion Architecture, Generic Repository, Specification Pattern |

## Project Structure

```text
ECommerceApi/
├── docker-compose.yml             # Orchestrates API, SQL Server, and Redis containers
├── ECommerceApi/                  # Presentation Layer (Controllers, Middlewares, Dockerfile)
├── ECommerceApi.Application/      # Application Layer (Services, DTOs, Exceptions)
├── ECommerceApi.Domain/           # Domain Layer (Entities, Core Interfaces, Domain Exceptions)
└── ECommerceApi.Infrastructure/   # Infrastructure Layer (EF Core, Repositories, DbContext)
ArchitectureThe project strictly follows Onion Architecture — all dependencies point inward toward the Domain layer:Domain: Entities and core interfaces. Zero external dependencies.Application: Business logic, DTOs, custom exceptions, and service interfaces. Depends only on Domain.Infrastructure: EF Core implementation, Generic Repository, and DbContext. Depends on Domain + Application.API: Controllers, exception middleware, Docker setup, and DI wiring. Depends on all layers.API EndpointsMethodEndpointDescriptionPOST/api/account/registerRegister a new userPOST/api/account/loginLogin and receive JWT tokenGET/api/productsGet all products (with filtering/sorting)GET/api/products/{id}Get product by IDGET/api/products/categoriesGet all categoriesPOST/api/basketAdd item to cartGET/api/basketGet current user's cartDELETE/api/basket/{id}Remove item from cartPOST/api/ordersPlace a new orderGET/api/ordersGet orders for current userGET/api/orders/{id}Get order by IDGET/api/admin/ordersGet all orders (Admin only)PATCH/api/admin/orders/{id}/statusUpdate order status (Admin only)Getting StartedPrerequisitesDocker Desktop (Required for running via Compose).NET 8 SDK (For local development/testing)Run with Docker (Recommended)This is the fastest way to get the API running along with SQL Server and Redis.Clone the repository:Bashgit clone [https://github.com/tantawyahmed578-web/ECommerceApi.git](https://github.com/tantawyahmed578-web/ECommerceApi.git)
cd ECommerceApi
Build and run the containers:Bashdocker compose up -d --build
Access the API:Swagger UI: http://localhost:8080/swaggerThe database and Redis will be automatically provisioned and connected.Run Locally (Manual Setup)If you prefer running the application without Docker:Ensure a local instance of SQL Server and Redis are running.Update the ConnectionStrings in ECommerceApi/appsettings.Development.json to point to your local instances.Restore packages and run EF Core migrations:Bashdotnet restore
cd ECommerceApi
dotnet ef database update --project ../ECommerceApi.Infrastructure --startup-project .
dotnet run
Architecture NotesTrue Decoupling: The Domain layer has zero external dependencies — only pure C# entities and interfaces. The data layer (SQL Server) or caching layer (Redis) could be entirely swapped without touching the Application or Domain layers.Testability: The Application layer holds all business logic and communicates with Infrastructure only through interfaces, making it fully testable with mocks.Centralized Error Handling: All exceptions are caught globally via .NET 8 IExceptionHandler, ensuring a consistent JSON response structure across the entire API without cluttering controllers.
