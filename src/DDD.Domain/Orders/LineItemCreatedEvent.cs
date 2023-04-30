namespace DDD.Domain.Orders;

public record LineItemCreatedEvent(LineItemId LineItemId, OrderId Order) : DomainEvent
{
    public LineItemCreatedEvent(LineItem lineItem) : this(lineItem.Id, lineItem.OrderId) { }
}