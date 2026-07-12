namespace homework_dependancy_injection.Repositories;

using homework_dependancy_injection.interfaces;

public class OrderRepository : IOrderRepository
{
    private readonly ILogger _logger;

    public OrderRepository(ILogger logger)
    {
        _logger = logger;
    }

    public void SaveOrder(string order)
    {
        // Simulate saving the order to a database
        _logger.Log($"Order saved: {order}");
    }
}