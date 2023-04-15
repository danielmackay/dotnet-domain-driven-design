using DDD.Domain.Common.Base;
using DDD.Domain.Common.Interfaces;

namespace DDD.Domain.Customers;

public class Customer : BaseEntity<CustomerId>, IAggregateRoot
{
    public string Email { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    // TODO: Turn this into a value object
    public string Address { get; init; } = string.Empty;

    private Customer() { }

    public static Customer Create(string email, string firstName, string lastName) => new()
    {
        Id = new CustomerId(Guid.NewGuid()),
        Email = email,
        FirstName = firstName,
        LastName = lastName
    };
}

public record CustomerId(Guid Value);
