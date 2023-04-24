using DDD.Domain.Products;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Products.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Handler}: Product {Name} was created", nameof(ProductCreatedEventHandler), notification.Product.Name);

        return Task.CompletedTask;
    }
}