# Data Access Layer Implementation

This document describes the Entity Framework Core data access layer implementation for the RedHorn application.

## Overview

The data access layer provides a clean, testable architecture for persisting questions submitted through the application. It uses:

- **Entity Framework Core 9.0** with SQL Server provider for production
- **EF Core InMemory provider** for development and testing
- **Repository pattern** for abstraction and testability
- **Async/await** for non-blocking database operations

## Architecture

### Models

#### Question Entity (`Models/Question.cs`)
```csharp
public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = default!;
    public string QuestionText { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
```

#### QuestionViewModel (`Models/QuestionViewModel.cs`)
Form binding model with validation attributes:
- Email: Required, must be valid email format
- QuestionText: Required, 10-1000 characters

### Data Access Layer

#### AppDbContext (`Data/AppDbContext.cs`)
EF Core DbContext that manages the Questions entity set.

#### IQuestionRepository (`Data/IQuestionRepository.cs`)
Repository interface with two methods:
- `AddAsync(Question question, CancellationToken ct)` - Persists a new question
- `GetByIdAsync(Guid id, CancellationToken ct)` - Retrieves a question by ID

#### EfQuestionRepository (`Data/EfQuestionRepository.cs`)
Implementation using Entity Framework Core.

### Controller

#### QuestionsController (`Controllers/QuestionsController.cs`)
- `GET /Questions/Ask` - Displays the question form
- `POST /Questions/Ask` - Processes form submission with validation
- `GET /Questions/Success` - Shows success message after submission

## Configuration

### Database Selection

The application uses a configuration flag to switch between database providers:

**Development** (`appsettings.Development.json`):
```json
{
  "UseInMemoryDatabase": true
}
```

**Production** (`appsettings.json`):
```json
{
  "UseInMemoryDatabase": false,
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RedHornDb;..."
  }
}
```

### Dependency Injection

In `Program.cs`, the database provider is configured based on environment:

```csharp
var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useInMemory && builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(opts =>
        opts.UseInMemoryDatabase("RedHornDev"));
}
else
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(opts =>
        opts.UseSqlServer(conn, sql => sql.EnableRetryOnFailure()));
}

builder.Services.AddScoped<IQuestionRepository, EfQuestionRepository>();
```

## Database Migration

For production deployment with SQL Server, apply the migration:

```bash
cd src/app/RedHorn.App
dotnet ef database update
```

The migration creates the `Questions` table with columns:
- `Id` (uniqueidentifier, PK)
- `Email` (nvarchar(max))
- `QuestionText` (nvarchar(max))
- `CreatedAt` (datetimeoffset)

## Running the Application

### Development (In-Memory Database)

```bash
cd src/app/RedHorn.App
dotnet run
```

The application will:
- Use the in-memory database (configured in `appsettings.Development.json`)
- Store data only in memory (data is lost on restart)
- Be available at http://localhost:5000

### Production (SQL Server)

1. Update the connection string in `appsettings.json` or use environment variables
2. Apply migrations: `dotnet ef database update`
3. Deploy the application to your hosting environment (Azure App Service, IIS, etc.)

For Azure deployment, store the connection string in:
- Azure App Service → Configuration → Connection strings
- Or use Azure Key Vault for better security

## Testing

### Unit Tests

The test suite includes:

**Repository Tests** (`EfQuestionRepositoryTests.cs`):
- Test adding questions to the database
- Test retrieving questions by ID
- Test handling of non-existent questions

**Controller Tests** (`QuestionsControllerTests.cs`):
- Test GET action returns view
- Test POST with valid model redirects to success
- Test POST with invalid model returns view with errors
- Test error handling when repository throws exceptions

Run tests:
```bash
cd src/tests/RedHorn.Tests
dotnet test
```

All tests use the EF Core InMemory provider or mocks for fast, isolated testing.

## Features

### Validation
- Client-side validation using jQuery Validation
- Server-side validation with DataAnnotations
- User-friendly error messages

### Error Handling
- Try-catch blocks around repository calls
- Logging of errors and successful operations
- Graceful degradation with user-facing error messages

### Performance
- Async/await throughout the data access layer
- SQL Server retry policy for transient failures
- Connection pooling enabled by default

## Security Considerations

1. **Connection Strings**: Never commit connection strings to source control
2. **SQL Injection**: EF Core parameterizes all queries automatically
3. **Input Validation**: All user input is validated
4. **HTTPS**: Enabled by default (configure in production)
5. **CSRF Protection**: ValidateAntiForgeryToken on POST actions

## Troubleshooting

### EF Core tools not found
```bash
dotnet tool install --global dotnet-ef
```

### Migration errors
Ensure the connection string is valid and SQL Server is accessible.

### In-memory database data disappears
This is expected behavior - data is only persisted during application runtime.

## Future Enhancements

Consider these improvements:

1. **Paging**: Add pagination for question lists
2. **Search**: Implement full-text search
3. **Admin Panel**: Add UI for viewing submitted questions
4. **Notifications**: Send email confirmations
5. **Rate Limiting**: Prevent spam submissions
6. **Cosmos DB**: For global distribution, consider migrating to Cosmos DB
7. **Caching**: Add Redis caching for frequently accessed data

## API Reference

### IQuestionRepository

```csharp
Task AddAsync(Question question, CancellationToken ct = default);
```
Adds a new question to the database.

```csharp
Task<Question?> GetByIdAsync(Guid id, CancellationToken ct = default);
```
Retrieves a question by its unique identifier.

## Related Documentation

- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)
- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/)
- [Repository Pattern](https://learn.microsoft.com/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
