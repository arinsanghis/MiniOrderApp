# Mini Order Management Application

## 📋 Overview
A backend API built with **.NET 10** demonstrating **Clean Architecture**, **CQRS**, and **Domain-Driven Design (DDD)** principles. The application manages Customers and Orders with strict business rules and validation.

## 🚀 Technologies
* **.NET 10** (Web API)
* **Entity Framework Core** (Code First)
* **SQLite** (for portability)
* **MediatR** (CQRS Pattern)
* **FluentValidation** (Validation Pipeline)
* **xUnit & Moq** (Unit Testing)
* **Swagger/OpenAPI** (Documentation)

## 🏗 Architecture & Design Patterns

### 1. Clean Architecture
The solution is strictly separated into concentric layers:
* **Domain:** The core. Contains Entities (`Customer`, `Order`) and Enums. No external dependencies.
* **Application:** Business logic. Contains CQRS Handlers, Validators, and Interfaces (`IUnitOfWork`).
* **Infrastructure:** External concerns. Implements EF Core `DbContext` and Repositories.
* **API:** Entry point. Thin controllers that delegate requests to MediatR.

### 2. CQRS (Command Query Responsibility Segregation)
Reads and Writes are separated:
* **Commands:** Change state (e.g., `CreateOrderCommand`).
* **Queries:** Retrieve data (e.g., `GetCustomerByIdQuery`).
* This decoupling allows independent scaling and simpler logic for each operation.

### 3. Repository & Unit of Work
* **Generic Repository:** Handles standard CRUD to reduce boilerplate.
* **Unit of Work:** Ensures database transactions are atomic. Multiple repositories share the same context, so changes are saved explicitly via `_unitOfWork.SaveChangesAsync()`.

### 4. DDD Approach
The **Customer** acts as an Aggregate Root. Relationships (1:1 with Profile, 1:N with Orders) are defined strictly in the Domain layer to ensure data integrity.

## 🛠 How to Run

### Prerequisites
* .NET SDK
* VS Code or Visual Studio

### Setup & Database
1.  Clone the repository.
2.  Navigate to the solution folder.
3.  Restore dependencies:
    ```bash
    dotnet restore
    ```
4.  Apply Database Migrations (Creates `miniorder.db`):
    ```bash
    dotnet ef database update --project Infrastructure --startup-project Api
    ```

### Running the API
```bash
dotnet run --project Api
```
Once running, open the Swagger UI to test endpoints:
👉 **http://localhost:5xxx/swagger**

## 🧪 Testing
The solution includes Unit Tests using **xUnit** and **Moq** to validate business logic in isolation.
Run tests with:
```bash
dotnet test
```
