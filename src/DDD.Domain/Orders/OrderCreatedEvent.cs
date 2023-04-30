using DDD.Domain.Customers;

namespace DDD.Domain.Orders;

public record OrderCreatedEvent(OrderId OrderId, CustomerId CustomerId) : DomainEvent
{
    public static OrderCreatedEvent Create(Order order) => new(order.Id, order.CustomerId);
}
