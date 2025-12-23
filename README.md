# Restaurant Ordering Backend API

## Overview

This project is a backend-focused **ASP.NET Core Web API** built to demonstrate
**production-oriented backend development practices**, including clean architecture,
domain-driven design concepts, and database-driven workflows, robust error handling, concurrency management, and testability.

The application models a **restaurant ordering domain**, focusing on
clear separation of concerns, maintainable business logic and scalable data access patterns.

## Architecture

The solution follows a **layered / clean architecture** approach:

- **API layer** – HTTP endpoints, request validation, DTO mapping
- **Application / Domain layer** – business logic, domain rules, use cases
- **Infrastructure layer** – data access (EF Core, Dapper), persistence, logging

Key design principles:
- Clear separation between API contracts (DTOs) and domain models
- Domain logic isolated from infrastructure concerns
- Explicit handling of business workflows and state transitions

## Business Logic

The backend implements **explicit business workflows** rather than simple CRUD operations.
Each operation validates domain rules before execution, ensuring data consistency.

Examples:
- Order creation and validation
- State transitions (e.g. Created → Confirmed → Completed)
- Side effects handled explicitly within the application layer

## Data Access

The project uses a **hybrid data access approach**:

- **Entity Framework Core** for standard CRUD operations and change tracking
- **Dapper-style queries** for read-heavy or performance-critical operations

This approach reflects real-world enterprise scenarios where different
data access strategies are combined based on performance and maintainability needs.

## API Design

- RESTful HTTP APIs
- Clear request/response DTOs
- Proper HTTP status codes

## Logging & Error Handling

- Structured logging is implemented across the application
- Centralized error handling at API level
- Logging is used to support troubleshooting and production diagnostics

## Testing

Unit testing is applied selectively to demonstrate
proper testing practices at the application and domain level.

The primary focus of this project is backend architecture,
business logic and data access patterns.

## Tech Stack

- C#
- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture
- DDD concepts
- REST
- xUnit

### Commit Types:
| Type | Meaning |
|----|--------|
| feat | new function |
| fix | bug |
| refactor | code refactoring |
| test | test modifications |
| chore | config, build |
| docs | documentactions |
