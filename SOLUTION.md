Problems Identified

--> Controllers were directly instantiating service implementations, leading to tight coupling and poor testability.
--> Business logic and database access logic were mixed in the same classes, violating separation of concerns.
--> SQLite connection strings were hard-coded and scattered across the codebase.
--> SQL queries were constructed using string interpolation, introducing potential security vulnerabilities.
--> API endpoints did not follow REST conventions or proper HTTP verb usage.
--> Tests relied on a real database, making them unreliable, slow, and order-dependent.

Architectural Decisions

--> Refactored the application to a layered architecture separating Controllers, Services, and Repositories.
--> Introduced interfaces to decouple components and enable dependency injection.
--> Moved all database access logic into a dedicated repository layer.
--> Kept services focused purely on business logic.
--> Refactored controllers to handle only HTTP request and response concerns.
--> Centralized configuration using appsettings.json and applied dependency injection consistently.

Trade-Offs

--> Chose raw SQLite with a repository pattern instead of using an ORM to keep data access explicit and easy to reason about.
--> Prioritized clean architecture and testability over adding new features.
--> Focused on unit tests instead of integration tests to ensure fast and deterministic feedback.
--> Deferred concerns such as validation, logging, and authentication to keep the scope aligned with the assignment.



How to Run
Prerequisites

--> .NET SDK 8.0 or higher

Build
dotnet build

Run
dotnet run --project TodoApi

Test
dotnet test


Future Improvements
--> Add request validation and centralized exception handling.
--> Introduce pagination and filtering for large TODO lists.
--> Add authentication and authorization.
--> Implement integration tests using an in-memory database.
--> Improve logging and observability.