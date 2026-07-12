# Custom Dependency Injection Container

This project is a lightweight, custom Dependency Injection (DI) container written in C#. It demonstrates the core principles of Inversion of Control (IoC) and dependency resolution by managing service lifetimes, resolving nested dependencies, and handling custom scopes.

## Supported Service Lifetimes

The container supports three primary service lifetimes:

- **Transient:** A new instance of the service is created every time it is requested from the container. Ideal for lightweight, stateless services.
- **Singleton:** A single instance is created the first time it is requested, and that exact same instance is reused for every subsequent request. Ideal for stateful services or expensive resources like configuration managers.
- **Scoped:** A single instance is created once per scope. If you request the service multiple times within the exact same scope, you get the same instance. Across different scopes, you get different instances. Ideal for web requests or database transactions.

## Project Structure

- `di-core/`: Contains the core engine for the DI container.
  - `CustomServiceCollection.cs`: Used to register services and their desired lifetimes.
  - `ServiceProvider.cs`: Responsible for resolving services, handling nested dependencies dynamically (via `Activator`), and managing Singleton and Scoped instance caches.
  - `ServiceDescriptor.cs`: Stores metadata about registered services (Type, Implementation, and Lifetime).
- `enums/`: Contains enumerations like `ServiceLifetime`.
- `interfaces/`: Contains abstraction definitions (e.g., `ILogger`, `IOrderRepository`, `IScopedService`).
- `repositories/` & `services/`: Contains implementations of your data access and business logic.

---

## How to Use the Container

### 1. Registering Services

Start by creating a `CustomServiceCollection`. This acts as your registry. You use it to map your interfaces to their concrete implementations and declare how long they should live.

```csharp
var serviceCollection = new CustomServiceCollection();

// Register a Transient service
serviceCollection.AddTransient<ITransientService, TransientService>();

// Register a Singleton service
serviceCollection.AddSingleton<ISingletonService, SingletonService>();

// Register a Scoped service
serviceCollection.AddScoped<IScopedService, ScopedService>();
```

### 2. Building the Service Provider

Once all your services and their dependencies are registered, you build the `ServiceProvider`. This provider is what you will actually use to request objects.

```csharp
var serviceProvider = serviceCollection.BuildServiceProvider();
```

### 3. Resolving Transient and Singleton Services

You can ask the `serviceProvider` directly for Transient and Singleton services. The container uses Reflection to automatically inspect the constructors of your requested services, resolve any nested dependencies, and inject them for you.

```csharp
// Returns a brand new instance every single time
var transient1 = serviceProvider.GetRequiredService<ITransientService>();
var transient2 = serviceProvider.GetRequiredService<ITransientService>();

// Returns the exact same instance every time
var singleton1 = serviceProvider.GetRequiredService<ISingletonService>();
var singleton2 = serviceProvider.GetRequiredService<ISingletonService>();
```

### 4. Working with Scoped Services

Scoped services are unique because their lifetime is tied to a specific "scope boundary" (which is highly useful in scenarios like handling HTTP requests, where you want one instance of a database context per request). 

To resolve a scoped service, you must first create a scope from the `ServiceProvider`.

```csharp
// Create the first scope
var scope1 = serviceProvider.CreateScope();
var scoped1A = scope1.GetRequiredService<IScopedService>();
var scoped1B = scope1.GetRequiredService<IScopedService>();

// scoped1A and scoped1B are the SAME instance because they were resolved from scope1.

// Create a second, completely separate scope
var scope2 = serviceProvider.CreateScope();
var scoped2A = scope2.GetRequiredService<IScopedService>();

// scoped2A is a DIFFERENT instance from scoped1A because it belongs to scope2.
```

## Running the Project

From the root directory, simply compile and run:

```bash
dotnet run
```
