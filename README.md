# 🛒 E-Commerce RESTful API (.NET Core)

A fully-featured, scalable RESTful API for an E-commerce platform built with **ASP.NET Core (.NET 9)**. The project follows **Onion Architecture** to ensure a clean separation of concerns and is deployed live on **Railway** using **PostgreSQL** for primary data persistence and **Redis** for high-performance caching.

🚀 **[Live Demo (Swagger UI) - Test it here!](https://lavish-tenderness-production-08cd.up.railway.app/swagger/index.html)**

---

## 🛠️ Tech Stack & Technologies

* **Framework:** .NET 9, ASP.NET Core Web API
* **Architecture:** Onion Architecture / Clean Architecture, Generic Repository Pattern, Unit of Work
* **Databases:** PostgreSQL (Relational Data), Redis (In-Memory Data Store for Shopping Basket)
* **ORM:** Entity Framework Core (EF Core)
* **Security:** JWT (JSON Web Tokens), BCrypt Password Hashing, Role-Based Authorization
* **Validation & Error Handling:** FluentValidation, Global Exception Handling Middleware
* **Mapping & Logging:** AutoMapper, Serilog
* **Deployment & DevOps:** Railway, Docker, Docker Compose, GitHub

---

## ✨ Key Features

* **Authentication & Authorization:** Secure user registration and login using JWT.
* **Product Catalog:** Fetch products with pagination, filtering, and search functionalities optimized at the database level.
* **Redis Shopping Basket:** High-performance caching for user shopping carts with automatic expiration.
* **Secure Checkout System:** Re-validates prices and stock directly from the database at checkout to prevent client-side data tampering.
* **Atomic Transactions:** Uses the Unit of Work pattern to ensure that stock deduction, order creation, and basket clearing succeed or fail together as a single transaction.

---

## 🧪 How to Test the Live API

You can test the API directly using the [Live Swagger Documentation](https://lavish-tenderness-production-08cd.up.railway.app/swagger/index.html). Follow these steps:

1. **Register/Login:** Go to `POST /api/Auth/register` to create a new user, then use `POST /api/Auth/login` to get your JWT Token.
2. **Authorize:** Copy the token, scroll to the top of the Swagger page, click the **Authorize** (lock) button, and paste the token.
3. **Explore Products:** Use `GET /api/Products` to view the available catalog.
4. **Update Basket:** Use `POST /api/Basket` to add a product to your Redis-backed cart.
5. **Place an Order:** Use `POST /api/Orders` to checkout and convert your basket into a confirmed order stored in PostgreSQL.

---

## 💻 Local Development Setup

If you want to run this project locally on your machine:

### Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (For PostgreSQL and Redis containers)

### Steps
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/ECommerce-API-DotNet.git](https://github.com/your-
   username/ECommerce-API-DotNet.git)
   cd ECommerce-API-DotNet


   Run Infrastructure via Docker:
Ensure Docker is running, then start the PostgreSQL and Redis containers:
    docker-compose up -d

    📐 Architecture Overview (Onion Architecture)
Domain Layer: Contains Enterprise Logic, Entities, and Interfaces. (No external dependencies).

Application Layer: Contains Business Logic, DTOs, Mapping profiles, and Validation. (Depends only on Domain).

Infrastructure Layer: Contains Data Access, EF Core DbContext, Repositories, and External Service Implementations.

API Layer: The presentation layer containing Controllers, Middlewares, and dependency injection setup.
