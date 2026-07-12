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