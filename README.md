# dotnet-ef-domain-driven-design

## DDD Principles

### Aggregate Roots

- Use AggregateRoots for objects that can be created directly
- All user interactions should go via AggregateRoots
- Exposed via DbContext
- Will have a table in the DB

### Entities

- Use Entities for objects that are part of an aggregate root and distinguisable by ID (usually a separate table)
- Entities cannot be modified outside of their aggregate root
- Not exposed via DbContext
- Will have a table in the DB
- Internal factory method for creation so that can only be created via aggregate roots

### Value Objects

- Use ValueObjects for objects that are defined by their properties (i.e. not by ID) (usually part of an another table)
- Value objects can only be modified as part of their aggregate root
- Will be part of the AggregateRoot table in the DB
- Configured in EF via `builder.OwnsOne()`

### General

- objects can be created using
  - factory methods
- All properties should be readonly (i.e. private)


## Features

- Aggregate Roots
- Entities
- Value Objects
- Strongly Typed IDs
- Domain events

## Key Design Decisions

### Prefer constructors over factory creation methods

Constructors are preferred as they are simpler and allow properties to be defined easier.

However, EF does not allow owned entities to be passed to constructors, so we will need to revert to factory methods in that case.

Factory Methods:

```cs
public class Customer : BaseEntity<CustomerId>, IAggregateRoot
{
    public required string Email { get; init }

    public required string FirstName { get; init }

    public required string LastName { get; init }

    public string? Address { get; }

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
}
```

Constructor:

```cs
public class Customer : BaseEntity<CustomerId>, IAggregateRoot
{
    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

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
```

### Use repositories to load aggregate roots

Aggregate roots should be loaded via repositories. This allows us to load the aggregate root and all of its related entities in a single query.  If we were to load the aggregate root directly via the DbContext, we would need to load each related entity separately and introduce possible errors by not loading all of the related entities.
