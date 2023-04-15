namespace DDD.Domain.Products;

public record ProductCreatedEvent(Product Product) : BaseEvent;