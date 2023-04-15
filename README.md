# dotnet-ef-domain-driven-design

## DDD Principles

- Use AggregateRoots for objects that can be created directly
- Use Entities for objects that are part of an aggregate root and distinguisable by ID (usually a separate table)
- Use ValueObjects for objects are defined by their properties (i.e. not by ID) (usually part of an another table)
- objects can be created using
  - public/internal constructors
  - factory methods
- All properties should be readonly (i.e. private)
- Entities cannot be modified outside of their aggregate root

## Features

- Aggregate Roots
- Entities
- Value Objects
- Strongly Typed IDs
- Domain events

## Key Design Decisions

- 
