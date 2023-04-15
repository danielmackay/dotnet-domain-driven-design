namespace DDD.Domain.Orders;

public record LineItemCreatedEvent(LineItem LineItem) : BaseEvent;