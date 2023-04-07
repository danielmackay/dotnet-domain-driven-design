using DDD.Domain.Interfaces;

namespace DDD.Domain.Customers;

public class Customer : IAggregateRoot
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    // TODO: Turn this into a value object
    public string Address { get; private set; } = string.Empty;
}
