# GitHub Copilot Guidelines for RedHorn

This document provides guidance for contributors on how to use GitHub Copilot effectively in this repository.

## Project Structure

- **src/app/RedHorn.App**: Main ASP.NET Core MVC application (.NET 8)
- **src/tests/RedHorn.Tests**: xUnit test project with references to the main app

## Development Workflow

### Using Copilot for Code Generation

1. **Be Specific**: Write clear comments describing what you need
   ```csharp
   // Create a controller action that returns a list of users sorted by creation date
   ```

2. **Context Matters**: Open related files to give Copilot better context
   - If creating a controller, have the corresponding model and view open
   - When writing tests, have the implementation file open

3. **Iterate on Suggestions**: Use `Ctrl+Enter` (or `Cmd+Enter` on Mac) to see multiple suggestions

### Testing with Copilot

1. **Test-Driven Development**: Write test method signatures first
   ```csharp
   [Fact]
   public void HomeController_Index_ReturnsViewResult()
   {
       // Copilot will suggest the implementation
   }
   ```

2. **Follow Naming Conventions**: Use descriptive test names
   - Format: `MethodName_Scenario_ExpectedBehavior`
   - Example: `Create_InvalidModel_ReturnsBadRequest`

3. **Use Arrange-Act-Assert Pattern**:
   but no need to add Arrange, Act And Assert comments
   ```csharp
   [Fact]
   public void Create_InvalidModel_ReturnsBadRequest()
   {

   }
   ```

### Business Components will be organized in /Business folder
### Data Components will be organized in /Data folder 


### Best Practices

1. **Controllers**: Keep them thin - delegate business logic to services
2. **Models**: Use data annotations for validation
3. **Views**: Follow Razor conventions and use tag helpers
4. **Styles**: Add custom CSS in `wwwroot/css/site.css`
5. **Tests**: Aim for high coverage of business logic and controllers

### Common Copilot Prompts

- "Create a controller with CRUD operations for [Model]"
- "Write xUnit tests for [ControllerName] with mock dependencies"
- "Generate a view model for [scenario]"
- "Add validation attributes to this model"
- "Create a responsive Razor view for [feature]"

### Running the Application
we develop on a windows machine with .NET 8 SDK installed
with pwsh

```pwsh
# Build the solution
dotnet build

# Run the web application
cd src/app/RedHorn.App
dotnet run

# Run tests
cd src/tests/RedHorn.Tests
dotnet test
```

### Git Commit messages
- Use the conventional commit format
  - feat: add new feature
  - fix: bug fix
  - docs: documentation changes
  - style: code style changes (formatting, etc.)
  - refactor: code refactoring without changing functionality
  - test: adding or updating tests
  - chore: maintenance tasks (build scripts, etc.)

