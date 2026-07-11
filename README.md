# Dependency Injection Homework

This project is a small custom dependency injection container written in C# to demonstrate how service lifetimes work.

## What It Supports

- `Transient` services create a new instance every time they are resolved.
- `Singleton` services are created once and reused for every later resolution.

## How It Works

Services are registered in `CustomServiceCollection`, then a `ServiceProvider` is built from those registrations.

Example usage:

```csharp
var serviceCollection = new CustomServiceCollection();
serviceCollection.AddTransient<ITransientService, TransientService>();
serviceCollection.AddSingleton<ISingletonService, SingletonService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var transient1 = serviceProvider.GetRequiredService<ITransientService>();
var transient2 = serviceProvider.GetRequiredService<ITransientService>();

var singleton1 = serviceProvider.GetRequiredService<ISingletonService>();
var singleton2 = serviceProvider.GetRequiredService<ISingletonService>();
```

The sample `Program.cs` prints the service IDs so you can see the difference:

- Transient services produce different IDs.
- Singleton services produce the same ID.

## Project Files

- `CustomServiceCollection.cs` handles service registration.
- `ServiceProvider.cs` resolves services and caches singleton instances.
- `ServiceDescriptor.cs` stores registration metadata and lifetimes.
- `Services.cs` contains the sample service interfaces and implementations.
- `Program.cs` shows the container in action.

## Run It

From the repository root:

```bash
dotnet run
```

## Planned Next Step

Scoped lifetime support will be added later.# HomeWork-Dependency-Injection
