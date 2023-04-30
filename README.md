# dotnet-ef-domain-driven-design

[![.NET](https://github.com/danielmackay/dotnet-ef-domain-driven-design/actions/workflows/dotnet.yml/badge.svg)](https://github.com/danielmackay/dotnet-ef-domain-driven-design/actions/workflows/dotnet.yml)

## Features

- Aggregate Roots
- Entities
- Value Objects
- Strongly Typed IDs
- Domain events
- CQRS Commands & Queries
- Fluent Validation
- Minimal APIs
- Specifications
- Outbox Pattern with Hangfire background processing

## Key Design Decisions

### Prefer constructors over factory creation methods

Constructors are preferred as they are simpler and allow properties to be defined easier.

However, EF does not allow owned entities to be passed to constructors, so we will need to revert to factory methods in that case.  Also, factories need to be used when raising creation domain events, so that events aren't raised when EF fetches entities from the DB.

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

### Use Specifications to load aggregate roots

Aggregate roots need to be loaded in their entirety.  This means the root entities and all child entities that make up the aggregate root.  To achieve this we will use specifications so that we can consistently load the aggregate root and all of its related entities in a single query.

### DomainService interfaces will need to exist in Domain

Sometimes entities will need to leverage a service to perform a behavior.  In these scenarios, we will need a DomainService interface in the Domain project, and implementation in the Application or Infrastructure project.

### Object Construction Constraints

- Objects must be constructed with a factory pattern so that domain events can be raised upon explicit creation, but NOT raised when EF fetches entities from the DB.
- Properties need to be passed to constructors to ensure they are in a valid state on object creation.  Can't use `required init` properties as they then become unmodifiable.
- EF does not allow owned entities to be passed to constructors, so these MUST be set via factory methods.
- Can remove nullable warnings by using `null!`.  This is safe to do so as we can only create an object via our factory method which we know sets these properties.

### Unit Test Naming Conventions

// Test Naming Convention: MethodName_StateUnderTest_ExpectedBehavior
// [Method/PropertyName]_Should_[ExpectedBehavior]_When_[StateUnderTest]
