# Mini Order Management System

## Solution Overview
A RESTful API designed for managing customers and orders, built with **.NET 10**. This solution enforces **Clean Architecture** principles to ensure separation of concerns, scalability, and testability.

The core domain logic is isolated from external frameworks, adhering to **Domain-Driven Design (DDD)** practices. Data consistency and business rules (e.g., validation pipelines) are enforced within the Application and Domain layers.

## Architecture & Design Decisions

### Clean Architecture
The solution is organized into four concentric layers:
* **Domain**: Contains enterprise logic, Entities (Aggregate Roots), and Value Objects. No external dependencies.
* **Application**: Orchestrates business use cases. Defines Interfaces (Abstractions) for infrastructure to implement.
* **Infrastructure**: Implements data access, file systems, and external services (EF Core DbContext, Repositories).
* **API**: The entry point (Presentation Layer). Controllers are thin and strictly delegate work to the Application layer.

### Pattern Implementations
* **CQRS (Command Query Responsibility Segregation)**: Implemented using **MediatR**. Write operations (Commands) and Read operations (Queries) are handled independently, allowing for optimized query models and distinct validation flows.
* **Repository & Unit of Work**: Wraps  to decouple the Application layer from EF Core. The Unit of Work pattern ensures atomic transactions across multiple repositories.
* **Validation**: **FluentValidation** is integrated into the request pipeline to enforce rules (e.g., non-empty names, positive order totals) before handlers execute.

## Tech Stack
* **.NET 10 (C#)**
* **Entity Framework Core** (Code-First)
* **SQLite** (Selected for portability and ease of local setup)
* **xUnit & Moq** (Unit Testing)
* **Swagger / OpenAPI**

## Setup Instructions

### Prerequisites
* .NET 10 SDK
* Git

### Installation & Database
1.  Restore dependencies:
    ```bash
    dotnet restore
    ```

2.  Apply Entity Framework migrations to create the local SQLite database (`miniorder.db`):
    ```bash
    dotnet ef database update --project Infrastructure --startup-project Api
    ```

### Running the Application
Launch the API:
```bash
dotnet run --project Api
```

Access the Swagger UI at:
**http://localhost:5xxx/swagger** (Port varies based on local Kestrel configuration, typically 5000-5200).

## Testing
The solution includes a dedicated Unit Test project covering command handlers and business logic. Dependencies such as the Repository and Unit of Work are mocked to ensure isolated testing.

Execute tests via:
```bash
dotnet test
```

## Project Structure
* **Api**: Controllers, Program.cs (DI Container), Middleware configuration.
* **Application**: CQRS Handlers, DTOs, Validator definitions, Interfaces.
* **Domain**: Entities (Customer, Order), Enums, Exceptions.
* **Infrastructure**: EF Core configurations, Migrations, Repository implementations.
