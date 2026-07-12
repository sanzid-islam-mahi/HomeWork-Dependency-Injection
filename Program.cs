using homework_dependancy_injection.di_core;
using homework_dependancy_injection.interfaces;
using homework_dependancy_injection.services;
using homework_dependancy_injection.Repositories;
namespace homework_dependancy_injection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new CustomServiceCollection();
        serviceCollection.AddTransient<ITransientService, TestServices>();
        serviceCollection.AddSingleton<ISingletonService, TestServices>();
        serviceCollection.AddScoped<IScopedService, TestServices>();

        serviceCollection.AddSingleton<ILogger, ConsoleLoggerService>();
        serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
        serviceCollection.AddSingleton<OrderService, OrderService>(); 

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var scope1 = serviceProvider.CreateScope();
        var scope2 = serviceProvider.CreateScope();
        var scopeService1 = scope1.GetRequiredService<IScopedService>();
        var scopeService2 = scope1.GetRequiredService<IScopedService>();
        var scopeService3 = scope2.GetRequiredService<IScopedService>();
        var scopeService4 = scope2.GetRequiredService<IScopedService>();

        scopeService1.PerformAction();
        scopeService2.PerformAction();
        scopeService3.PerformAction();
        scopeService4.PerformAction();



    }
}