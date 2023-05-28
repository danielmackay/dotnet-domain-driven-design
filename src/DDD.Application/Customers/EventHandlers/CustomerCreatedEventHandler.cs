using Microsoft.Extensions.Logging;

namespace DDD.Application.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger) => _logger = logger;

    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{Handler}: Customer {FirstName} {LastName} was created", nameof(CustomerCreatedEventHandler), notification.FirstName, notification.LastName);

        return Task.CompletedTask;
    }
}