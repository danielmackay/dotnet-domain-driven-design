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

### DomainService interfaces will need to exist in Domain

Sometimes entities will need to leverage a service to perform a behavior.  In these scenarios, we will need a DomainService interface in the Domain project, and implementation in the Application or Infrastructure project.

### Unit Test Naming Conventions

Test naming convention should follow the pattern

```cs
public void {Method/PropertyName}_Should_{ExpectedBehavior}_When_{StateUnderTest}()
```

### Don't wrap domain objects in domain events

Due to using the Outbox Message pattern domain events need to be serialized and deserialized.  Domain objects can only be created using factory methods which caused problems with deserialization as all properties have private setters.  To get around this we will not wrap domain objects in domain events, but instead, pass the properties of the domain object to the domain event.
