namespace DDD.Domain.Customers;

public record CustomerCreatedEvent(CustomerId Id, string FirstName, string LastName) : DomainEvent
{
    public static CustomerCreatedEvent Create(Customer customer) =>
        new CustomerCreatedEvent(customer.Id, customer.FirstName, customer.LastName);
}