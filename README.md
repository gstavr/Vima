# RESTful API Project with .NET Core

## Overview
This project involves the creation of a RESTful API using .NET Core (version 5 or above), designed to demonstrate modern API development techniques including CRUD operations, database management, and comprehensive application of design patterns and principles.

## Key Technologies
- **.NET Core (5 or above)**: Employs the robust framework for building high-performance internet services.
- **Entity Framework Core**: Uses ORM with a Code First approach for effective database migrations.
- **Swagger**: Provides enhanced API documentation and a user-friendly interface for testing API endpoints.
- **Dependency Injection (DI)**: Integral to the project, ensuring loose coupling and improved testability of components.

## Software Design Principles and Patterns
- **DRY (Don't Repeat Yourself)**: Avoids redundancy to enhance code maintenance and scalability.
- **KISS (Keep It Simple, Stupid)**: Maintains simplicity in design to facilitate ease of management and understanding.
- **Repository Pattern**: Abstracts data access to unify data operations and provide a clear separation of concerns.
- **Dependency Injection**: Used extensively throughout the project to manage class dependencies, enhance modularity, and facilitate easier unit testing.
- **Additional Patterns**: Includes various other design patterns tailored to specific requirements, optimizing code efficiency and maintainability.

## Base Requirements
- **Database Initialization**: Automatically creates and seeds the database if it does not exist.
- **JSON Communication**: All API requests and responses are strictly in JSON format.
- **CRUD Operations**:
  - `Create`: Allows for the creation of entities such as Users, Books, and Categories.
  - `Read`: Facilitates retrieval of single or multiple entities including Users, Books, and Categories.
  - `Update`: Permits updates to entity details like User roles or Book information.
  - `Delete`: Provides the ability to remove entities such as Users or Categories.

## Advanced Features
- **API Versioning**: Ensures the API can evolve without disrupting existing client integrations.
- **Fluent API Configurations**: Employs detailed configurations for Entity Framework models to refine ORM behavior.
- **C# Filters**: Implements centralized logging and error handling across API operations.
- **Audit Logging**: Captures detailed records of data modifications for accountability and compliance.
- **Scheduled Maintenance**: Regularly clears audit log entries older than twenty days to manage storage efficiently.

## Optional Enhancements
- **Audit Trail**: Every modification (create, update, delete) is logged with details including action type, timestamp, and actor identity.
- **Automated Log Cleanup**: A scheduled job automatically purges older audit log entries, maintaining a relevant and lean database.

## Testing Strategy
- **Unit Testing**: Comprehensive testing covers essential functionalities, ensuring the API operates reliably under various scenarios.

## Deployment
- **Docker Integration**: The API is fully containerized, simplifying deployment processes and ensuring environment consistency.

### Quick Start with Docker
```bash
docker build -t myapi .
docker run -p 8000:80 myapi
