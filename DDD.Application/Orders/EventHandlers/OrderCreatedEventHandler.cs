using DDD.Domain.Orders;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Handler}: Order was created for Cusomter {CustomerId}", nameof(OrderCreatedEventHandler), notification.Order.CustomerId.Value);

        return Task.CompletedTask;
    }
}