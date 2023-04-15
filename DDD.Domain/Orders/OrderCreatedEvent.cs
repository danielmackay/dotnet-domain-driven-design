namespace DDD.Domain.Orders;

public record OrderCreatedEvent(Order Order) : BaseEvent;