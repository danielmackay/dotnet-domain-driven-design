# Don't Store Aggregates Directly in Domain Events

- Status: accepted
- Deciders: Daniel Mackay

## Context and Problem Statement

When raising domain events the events themselves need to store data.  These events will be serialized / deserialized when they are processed by the outbox pattern.

## Considered Options

1. Wrap aggregates in domain events
2. Store properties individually

## Decision Outcome

Chosen option: "2 - Store properties individually", because it allows the outbox pattern to deserialize the domain events.

## Pros and Cons of the Options <!-- optional -->

### 1. Wrap aggregates in domain events

- ✅ Easy to create and publish domain events
- ❌ Outbox pattern can't deserialize the domain events due to the private constructors

### 2. Store properties individually

- ✅ Outbox pattern can deserialize the domain events
- ❌ More verbose implementation
