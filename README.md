# dotnet-ef-domain-driven-design

## Features

- Aggregate Roots
- Entities
- Value Objects
- Strongly Typed IDs
- Domain events
- CQRS Commands & Queries
- Fluent Validation
- Minimal APIs

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
- Behaviors should be internal so that they can only be called by the aggregate root

### Value Objects

- Use ValueObjects for objects that are defined by their properties (i.e. not by ID) (usually part of an another table)
- Value objects can only be modified as part of their aggregate root
- Will be part of the AggregateRoot table in the DB
- Configured in EF via `builder.OwnsOne()`

### General

- objects can be created using
  - factory methods
- All properties should be readonly (i.e. private)




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

### DomainService interfaces will need to exist in Domain

Sometimes entities will need to leverage a service to perform a behavior.  In these scenarios we will need a DomainService interface in the Domain project, and an implementation in the Application or Infrastructure project.





## Thoughts

- Once you start relying on aggregates being loaded as 'entity sets', they must be loaded as such.  While this is possible with EF, it is error-prone if you need to load the same aggregate in multiple places.  To get around this you need to use a repository to load the aggregate root and all of its related entities in a single query.
- Initially I thought we could use repositories for commands, and then use the DbContext directly for queries, but if you're queries rely on any computed properies that rely on the whole aggregate this isn't possible.  Could get around this though, by not using computed properies and instead storing the computed value in the DB.
- Can't pass owned entities to constructors, so will need to use factory methods for those.
- Business logic and validation can now be easily unit tested in isolation via AggregateRoot tests.
- DDD + CQRS is a heavy weight solution.  It gives us great control by ensuring entites are always in a valid state, but there is additional complexity across both the Domain and Application that are required to support this.  Perhaps only ideal for 'Large' projects.
- If you have a massive Aggregate root, but just need to update a simple property on one of the entities, you will need to load the whole aggregate root.  This could be a performance issue.
- Some interfaces need to be pushed to the domain layer
- IMHO the repository interface remains in the application, NOT the domain as this is a persistence concern.  The domain should not be concerned with persistence.
