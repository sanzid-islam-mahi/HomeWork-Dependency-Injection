# Dependency Injection Homework

This project is a small custom dependency injection container written in C# to demonstrate how service lifetimes work.

## What It Supports

- `Transient` services create a new instance every time they are resolved.
- `Singleton` services are created once and reused for every later resolution.

## How It Works

Services are registered in `CustomServiceCollection`, then a `ServiceProvider` is built from those registrations. The current implementation demonstrates resolving an `OrderService` that depends on `IOrderRepository` and `ILogger`.

Example usage from `Program.cs`:

```csharp
using homework_dependancy_injection.services;
using homework_dependancy_injection.interfaces;
using homework_dependancy_injection.Repositories;
using homework_dependancy_injection.DependencyInjection;

namespace homework_dependancy_injection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new CustomServiceCollection();
        serviceCollection.AddSingleton<ILogger, ConsoleLoggerService>();
        serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
        serviceCollection.AddSingleton<OrderService, OrderService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var orderService = serviceProvider.GetRequiredService<OrderService>();
        orderService.PlaceOrder("Order1");
    }
}
```

## Project Structure

The project has been refactored into the following components:

- `di-core/`
  - `CustomServiceCollection.cs`: handles service registration.
  - `ServiceProvider.cs`: resolves services, resolves their dependencies, and caches singleton instances.
  - `ServiceDescriptor.cs`: stores registration metadata (e.g., service type, implementation type, and lifetime).
- `enums/`: contains enumerations like `ServiceLifetime`.
- `interfaces/`: contains the abstraction definitions (e.g., `ILogger`, `IOrderRepository`).
- `repositories/`: contains the implementations of the data access layer (e.g., `OrderRepository`).
- `services/`: contains the implementations of the business logic (e.g., `ConsoleLoggerService`, `OrderService`).
- `Program.cs`: the main entry point that wires up the container and runs the application.

## Run It

From the repository root:

```bash
dotnet run
```

## Planned Next Steps

Scoped lifetime support will be added later.
