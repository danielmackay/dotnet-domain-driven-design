using DDD.Domain.Common.Extensions;

namespace DDD.Domain.Customers;

public class Customer : BaseEntity<CustomerId>, IAggregateRoot
{
    public string Email { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    // TODO: Turn this into a value object
    public string? Address { get; private set; }

    private Customer() { }

    public static Customer Create(string email, string firstName, string lastName)
    {
        var customer = new Customer()
        {
            Id = new CustomerId(Guid.NewGuid()),
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
        customer.AddDomainEvent(new CustomerCreatedEvent(customer));
        return customer;
    }

    public void UpdateName(string firstName, string lastName)
    {
        DomainException.ThrowIf(firstName.IsEmpty(), $"{nameof(firstName)} cannot be empty");
        DomainException.ThrowIf(lastName.IsEmpty(), $"{nameof(lastName)} cannot be empty");

        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateAddress(string address) => Address = address;
}