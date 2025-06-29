# FeatureFlagsDemo

## Overview

**FeatureFlagsDemo** is a sample ASP.NET Core project that demonstrates the usage of the [Microsoft.FeatureManagement](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-overview) library for implementing feature flags (feature toggles) in modern .NET applications. Feature flags allow you to enable or disable application features at runtime without deploying new code, making it easier to perform gradual rollouts, A/B testing, and safe deployments.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Setup & Prerequisites](#setup--prerequisites)
- [Configuration](#configuration)
- [Feature Flags Usage](#feature-flags-usage)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Extending Feature Flags](#extending-feature-flags)
- [References](#references)
- [License](#license)

---

## Features

- Demonstrates feature flag management using Microsoft.FeatureManagement.
- Toggle features on/off via configuration.
- Example controller and middleware for feature-based access.
- Unit tests for feature flag logic and endpoints.
- Ready for extension with new flags and features.

---

## Architecture

```
FeatureFlagsDemo.sln
├── FeatureFlagsDemo/           # Main ASP.NET Core Web API project
│   ├── Controllers/            # API controllers
│   ├── Middleware/             # Custom middleware (e.g., feature toggle)
│   ├── Models/                 # Data models
│   ├── Services/               # Feature flag services
│   ├── appsettings.json        # Main configuration (feature flags defined here)
│   └── Program.cs              # Entry point and DI setup
└── FeatureFlagsDemoTests/      # xUnit test project
    └── ...                     # Test classes for controllers, services, middleware
```

---

## Setup & Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022+ or VS Code

**To build and run:**

```sh
dotnet restore
dotnet build
dotnet run --project FeatureFlagsDemo/FeatureFlagsDemo.csproj
```

---

## Configuration

Feature flags are defined in [`FeatureFlagsDemo/appsettings.json`](FeatureFlagsDemo/appsettings.json):

```json
{
  "FeatureManagement": {
    "EnableNewDashboard": true,
    "UseAdvancedSearch": false,
    "BlockLegacyEndpoints": true
  }
}
```

- Set each flag to `true` or `false` to enable/disable features.
- Use `appsettings.Development.json` for environment-specific overrides.

---

## Feature Flags Usage

- Flags are injected via DI using `IFeatureManager`.
- Example usage in controllers and middleware to conditionally enable features.
- Easily add new flags by updating configuration and service logic.

---

## API Endpoints

Example endpoints (see [`FeatureFlagsDemo/Controllers/`](FeatureFlagsDemo/Controllers/)):

- `GET /api/features/{featureName}`  
  Returns the status of a feature flag.

- `GET /api/features/legacy`  
  Example endpoint protected by a feature flag.

---

## Testing

Unit tests are located in [`FeatureFlagsDemoTests/`](FeatureFlagsDemoTests/):

- Uses [xUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq4).
- Test coverage for feature toggle logic, controller endpoints, and middleware.
- To run tests:

```sh
dotnet test
```

---

## Extending Feature Flags

1. **Add a new flag** in `appsettings.json` under `FeatureManagement`.
2. **Expose a new method** in your feature toggle service and its interface.
3. **Add a new API endpoint** in your controller if needed.
4. **(Optional)** Add middleware to protect specific routes.

---

## References

- [Microsoft.FeatureManagement Documentation](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-overview)
- [Feature Flags Best Practices](https://martinfowler.com/articles/feature-toggles.html)
- [ASP.NET Core Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/)
- [xUnit Testing](https://xunit.net/)

---

## License

This project is for demonstration purposes only.