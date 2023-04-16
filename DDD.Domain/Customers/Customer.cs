namespace DDD.Domain.Customers;

public class Customer : BaseEntity<CustomerId>, IAggregateRoot
{
    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    // TODO: Turn this into a value object
    public string? Address { get; }

    public Customer(string email, string firstName, string lastName)
        : base(new CustomerId(Guid.NewGuid()))
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;

        AddDomainEvent(new CustomerCreatedEvent(this));
    }
}
