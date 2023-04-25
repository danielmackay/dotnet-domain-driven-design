namespace DDD.Domain.Customers;

public record CustomerCreatedEvent(Customer Customer) : DomainEvent;