# Unit Tests will use the 'Should' 'When' naming convention

- Status: accepted
- Deciders: Daniel Mackay

## Context and Problem Statement

Unit tests should use a consistent naming convention so they are easy to read and understand.

## Decision Drivers <!-- optional -->

- Consistency
- Obvious naming pattern

## Decision Outcome

Test naming convention should follow the pattern:

```cs
public void {Method/PropertyName}_Should_{ExpectedBehavior}_When_{StateUnderTest}()
```
