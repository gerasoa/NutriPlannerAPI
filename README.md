# NutriPlanner API

RESTful API built with **ASP.NET Core (.NET 8)** designed to manage
nutrition consultations, doctors, patients and appointments.

This project demonstrates **clean architecture principles**, **layered
architecture**, and modern backend development practices including **JWT
authentication**, **Entity Framework Core**, and **Swagger/OpenAPI
documentation**.

------------------------------------------------------------------------

## Technologies

-   ASP.NET Core (.NET 8)
-   C#
-   Entity Framework Core
-   SQL Server
-   JWT Authentication
-   Swagger / OpenAPI
-   AutoMapper
-   FluentValidation

------------------------------------------------------------------------

## Architecture

The project follows a **layered architecture** separating
responsibilities across three main projects:

    CCR.Api → API layer (controllers, endpoints, configuration)
    CCR.Business → Business logic, services and domain models
    CCR.Data → Data access, repositories, Entity Framework context

This structure improves **maintainability, scalability and
testability**.

------------------------------------------------------------------------

## Project Structure

### API Layer

Handles HTTP requests, API configuration and routing.

Main components:

-   Controllers
-   API Versioning
-   ViewModels
-   Configuration
-   Extensions
-   Middleware

```{=html}
<!-- -->
```
    CCR.Api
    │
    ├── Controllers
    ├── Configuration
    ├── Extensions
    ├── ViewModels
    ├── V1 / V2
    ├── Program.cs
    └── appsettings.json

------------------------------------------------------------------------

### Business Layer

Contains business rules, domain models and service logic.

    CCR.Business
    │
    ├── Models
    │   ├── Doctor
    │   ├── Patient
    │   ├── Appointment
    │
    ├── Services
    ├── Interfaces
    ├── Validations
    └── Notifications

------------------------------------------------------------------------

### Data Layer

Responsible for persistence and database access.

    CCR.Data
    │
    ├── Context
    ├── Repository
    ├── Mappings
    └── Migrations

------------------------------------------------------------------------

## Features

-   Doctor management
-   Patient management
-   Appointment scheduling
-   Authentication using JWT
-   Data validation with FluentValidation
-   Clean architecture with separation of concerns
-   Swagger API documentation

------------------------------------------------------------------------

## Authentication

Authentication is implemented using **JWT tokens**.

After login, the API returns a token that must be included in the
request header:

    Authorization: Bearer {token}

------------------------------------------------------------------------

## Running the Project

### 1. Clone repository

    git clone https://github.com/gerasoa/NutriPlannerAPI

### 2. Configure database

Update the connection string in:

    appsettings.json

Example:

    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=NutriPlannerDB;Trusted_Connection=True;"
    }

### 3. Apply migrations

    dotnet ef database update

### 4. Run application

    dotnet run

------------------------------------------------------------------------

## API Documentation

Swagger documentation is available when running the project:

    https://localhost:{port}/swagger

------------------------------------------------------------------------

## Example Endpoints

    GET /api/v1/doctors
    POST /api/v1/patients
    POST /api/v1/appointments
    GET /api/v1/appointments

------------------------------------------------------------------------

## Design Principles

This project follows:

-   SOLID Principles
-   Separation of Concerns
-   Repository Pattern
-   Service Layer Pattern
-   Clean Code practices

------------------------------------------------------------------------

## Author

**Rogerio Alves Soares**

Software Engineer focused on backend development with **C#, ASP.NET Core
and REST APIs**.

LinkedIn\
https://www.linkedin.com/in/rogerio-alves-soares/

GitHub\
https://github.com/gerasoa
