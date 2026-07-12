namespace homework_dependancy_injection.services;

using homework_dependancy_injection.interfaces;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;

    public OrderService(IOrderRepository orderRepository, ILogger logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public void PlaceOrder(string order)
    {
        _logger.Log($"Order placed: {order}");
        _orderRepository.SaveOrder(order);
    }
}