namespace DDD.Domain.Orders;

public record OrderReadyForShippingEvent(OrderId OrderId) : DomainEvent
{
    public OrderReadyForShippingEvent(Order order) : this(order.Id) { }
}
