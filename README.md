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

- TBC
