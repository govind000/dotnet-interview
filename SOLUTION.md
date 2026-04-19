Problems Identified



\--> Controllers were directly instantiating service implementations, leading to tight coupling and poor testability.

\--> Business logic and database access logic were mixed in the same classes, violating the separation of concerns.

\--> SQLite connection strings were hard-coded and scattered across the codebase.

\--> API endpoints did not follow REST conventions or proper HTTP verb usage.

\--> Tests relied on a real database, making them unreliable, slow, and order-dependent.



Architectural Decisions



\--> Introduced a layered architecture by separating Controllers, Services, and Repositories.

\--> Introduced interfaces to decouple components and enable dependency injection.

\--> Moved all database access logic into a dedicated repository layer.

\--> Kept services focused purely on business logic.

\--> Refactored controllers to handle only HTTP request and response concerns.



Trade-Offs



\--> I chose to use raw SQLite with the repository pattern instead of an ORM like Entity Framework. This keeps the data access simple and explicit, although it requires more manual effort.

\--> Prioritized clean architecture and testability over adding new features.

\--> I focused more on code quality, structure, and testability rather than adding new features.





How to Run

Prerequisites



\--> .NET SDK 8.0 or higher



Build

dotnet build



Run

dotnet run --project TodoApi



Test

dotnet test





Future Improvements

\--> Adding proper request validation and centralized exception handling.

\--> Implementing authentication and authorization.

\--> Implement integration tests using an in-memory database.

