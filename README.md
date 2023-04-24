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

## DDD Principles

### Aggregate Roots

- Use AggregateRoots for objects that can be created directly
- All user interactions should go via AggregateRoots
- Exposed via DbContext
- Will have a table in t
- Aggregates can only be pulled in their entirety from the DB.  This means that all child entities that make up the aggregate root will also be pulled in

### Entities

- Use Entities for objects that are part of an aggregate root and distinguishable by ID (usually a separate table)
- Entities **cannot be modified outside** of their aggregate root
- Not exposed via DbContext
- Will have a table in the DB
- Internal factory method for creation so that can only be created via aggregate roots
- Behaviors should be internal so that they can only be called by the aggregate root

### Value Objects

- Use ValueObjects for objects that are defined by their properties (i.e. not by ID) (usually part of another table)
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

## Thoughts

- Once you start relying on aggregates being loaded as 'entity sets', they must be loaded as such.  While this is possible with EF, it is error-prone if you need to load the same aggregate in multiple places.  To get around this you need to use a repository to load the aggregate root and all of its related entities in a single query.
- Initially, I thought we could use repositories for commands, and then use the DbContext directly for queries, but if your queries rely on any computed properties that rely on the whole aggregate this isn't possible.  Could get around this though, by not using computed properties and instead storing the computed value in the DB.
- Can't pass owned entities to constructors, so will need to use factory methods for those.
- Business logic and validation can now be easily unit tested in isolation via AggregateRoot tests.
- DDD + CQRS is a heavy-weight solution.  It gives us great control by ensuring entities are always in a valid state, but there is additional complexity across both the Domain and Application that are required to support this.  Perhaps only ideal for 'Large' projects.
- If you have a massive Aggregate root, but just need to update a simple property on one of the entities, you will need to load the whole aggregate root.  This could be a performance issue.
- Some interfaces need to be pushed to the domain layer
- IMHO the repository interface remains in the application, NOT the domain as this is a persistence concern.  The domain should not be concerned with persistence.
- IMHO we should not have separate domain entities and data models.  We can keep the domain entities as they are, but we can use EF to configure the persistence. This saves on a lot of double handling.
