namespace DDD.Domain.Categories;

public record CategoryCreatedEvent(CategoryId Id, string Name) : DomainEvent;