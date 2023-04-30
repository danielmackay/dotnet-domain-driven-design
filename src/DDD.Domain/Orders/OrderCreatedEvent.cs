using DDD.Domain.Customers;

namespace DDD.Domain.Orders;

public record OrderCreatedEvent(OrderId OrderId, CustomerId CustomerId) : DomainEvent
{
    public OrderCreatedEvent(Order order) : this(order.Id, order.CustomerId) { }
}
