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

## End-to-end tests (Playwright)

This repository includes Playwright end-to-end (E2E) tests under `src/tests/e2e` that exercise the running web application.

Prerequisites:
- Node.js (LTS recommended)
- Playwright (browsers can be installed with `npx playwright install`)

Common commands (PowerShell):

```pwsh
# Run the full Playwright suite (the configured webServer will start the app)
npx playwright test

# Run only the example e2e file
npx playwright test src/tests/e2e/example.spec.ts -c playwright.config.ts

# Run tests in Chromium only (faster local runs)
npx playwright test -p chromium

# Install Playwright browsers (first time only)
npx playwright install

# Show HTML report after a test run
npx playwright show-report
```

Notes:
- `playwright.config.ts` contains a `webServer` entry that runs the application with `dotnet run --project ./src/app/RedHorn.App/RedHorn.App.csproj` and waits for `http://localhost:5033`.
- The E2E tests assert on headings, labels and messages rendered by the Razor views and use Playwright's accessibility selectors (for example `getByRole`, `getByLabel`).
- For quick local iterations, run only the Chromium project with `-p chromium` or use the `-g` flag to filter tests by name.

If you prefer a convenience npm script, I can add `"test:e2e": "playwright test"` to `package.json`.

## Development

### Running in Watch Mode

For hot reload during development:

```
cd src/app/RedHorn.App
dotnet watch run
```

### Adding New Dependencies

To add a NuGet package to the main application:

```
cd src/app/RedHorn.App
dotnet add package <PackageName>
```

To add a NuGet package to the test project:

```
cd src/tests/RedHorn.Tests
dotnet add package <PackageName>
```

### Creating New Controllers

```
# Navigate to the app directory
cd src/app/RedHorn.App

# Controllers are typically created manually or through scaffolding
# Example structure:
# Controllers/YourController.cs
```

## Testing

The project uses xUnit for testing. Tests are located in `src/tests/RedHorn.Tests`.

### Running Specific Tests

```
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
