namespace DDD.Domain.Customers;

public record CustomerCreatedEvent(Customer Customer) : BaseEvent;
//{
//    public CustomerCreatedEvent(Customer customer) => Customer = customer;

//    public Customer Customer { get; }
//}