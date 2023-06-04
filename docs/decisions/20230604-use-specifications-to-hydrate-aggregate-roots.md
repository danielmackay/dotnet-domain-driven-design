# Use Specifications to Hydrate Aggregate Roots

- Status: accepted
- Deciders: Daniel Mackay

## Context and Problem Statement

When hydrating an aggregate from the DB we need to ensure that the aggregate and all it's child entities are loaded in a single query.  This is to ensure that the aggregate is in a valid state.  We want to have this logic centralised so we can guarantee the aggregate is hydrated correctly.

## Considered Options

1. Specifications
2. EF Includes
3. Factory Methods

## Decision Outcome

Chosen option: "1. Specifications", because it allows us to succinctly express the query to load the aggregate root and all child entities.  We can also store the specification along side the aggregate root so these things can change together.

### Consequences

- Need to introduce a depdenency on the `Specification` package into the `Domain` layer 

## Pros and Cons of the Options

### 1. Specifications

- ✅ Loading logic is centralised
- ✅ Specifications can live beside the aggregate root.  If the aggregate root changes, so can the specification
- ✅ Composable with EF Core
- ❌ Need to introduce a depdenency on the `Specification` package into the `Domain` layer 

### 2. EF Includes

- ✅ No need to introduce a depdenency on the `Specification` package into the `Domain` layer
- ❌ Loading logic is not centralised.  Easy to load the aggregate root without all child entities

### 3. Factory Methods

- ✅ No need to introduce a depdenency on the `Specification` package into the `Domain` layer
- ✅ Loading logic is centralised
- ❌ Loading logic is not succintly expressed (verbose implementation)
