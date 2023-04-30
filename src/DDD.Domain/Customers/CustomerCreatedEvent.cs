namespace DDD.Domain.Customers;

public record CustomerCreatedEvent(CustomerId Id, string FirstName, string LastName) : DomainEvent
{
    public CustomerCreatedEvent(Customer customer) : this(customer.Id, customer.FirstName, customer.LastName) { }
}