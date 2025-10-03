# RedHorn

A modern ASP.NET Core MVC application built with .NET 8, featuring a clean architecture and comprehensive test coverage.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A code editor (Visual Studio 2022, VS Code, or JetBrains Rider)
- Git

## Project Structure

```
RedHorn/
├── src/
│   ├── app/
│   │   └── RedHorn.App/          # Main ASP.NET Core MVC application
│   │       ├── Controllers/       # MVC Controllers
│   │       ├── Models/            # Data models and view models
│   │       ├── Views/             # Razor views
│   │       └── wwwroot/           # Static files (CSS, JS, images)
│   └── tests/
│       └── RedHorn.Tests/         # xUnit test project
├── .github/
│   └── copilot-instructions.md    # GitHub Copilot guidelines
└── README.md
```

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/rob-foulkrod/RedHorn.git
cd RedHorn
```

### 2. Build the Solution

```bash
dotnet build
```

### 3. Run the Application

```bash
cd src/app/RedHorn.App
dotnet run
```

The application will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 4. Run Tests

```bash
cd src/tests/RedHorn.Tests
dotnet test
```

## Development

### Running in Watch Mode

For hot reload during development:

```bash
cd src/app/RedHorn.App
dotnet watch run
```

### Adding New Dependencies

To add a NuGet package to the main application:

```bash
cd src/app/RedHorn.App
dotnet add package <PackageName>
```

To add a NuGet package to the test project:

```bash
cd src/tests/RedHorn.Tests
dotnet add package <PackageName>
```

### Creating New Controllers

```bash
# Navigate to the app directory
cd src/app/RedHorn.App

# Controllers are typically created manually or through scaffolding
# Example structure:
# Controllers/YourController.cs
```

## Testing

The project uses xUnit for testing. Tests are located in `src/tests/RedHorn.Tests`.

### Running Specific Tests

```bash
cd src/tests/RedHorn.Tests

# Run a specific test
dotnet test --filter "FullyQualifiedName~YourTestName"

# Run tests with verbose output
dotnet test --verbosity detailed
```

### Test Coverage

To generate test coverage reports, you can use tools like:
- [Coverlet](https://github.com/coverlet-coverage/coverlet)
- [ReportGenerator](https://github.com/danielpalme/ReportGenerator)

## Using GitHub Copilot

This repository includes guidelines for using GitHub Copilot effectively. See [.github/copilot-instructions.md](.github/copilot-instructions.md) for:
- Code generation best practices
- Testing patterns
- Common prompts and workflows

## Features

- ✨ Modern, responsive UI with Bootstrap 5
- 🏗️ Clean MVC architecture
- 🧪 Comprehensive xUnit test setup
- 🚀 .NET 8 performance and features
- 📝 Razor views with tag helpers
- 🎨 Custom CSS styling

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For questions or issues, please open an issue on GitHub.
