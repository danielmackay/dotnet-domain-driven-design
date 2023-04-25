namespace DDD.Domain.Orders;

public record OrderReadyForShippingEvent(Order Order) : DomainEvent;
