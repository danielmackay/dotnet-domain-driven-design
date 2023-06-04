# Use Domain Services To Encapsulate External Behavior

- Status: accepted
- Deciders: Daniel Mackay

## Context and Problem Statement

Sometimes aggregates need to leverage functionality that cannot be implemented in the `Domain` (e.g. `Infrastructure` concerns).  

## Decision Outcome

We need to introduce a `DomainService` interface in the `Domain` project, and an implementation in the `Application` or `Infrastructure` project.
