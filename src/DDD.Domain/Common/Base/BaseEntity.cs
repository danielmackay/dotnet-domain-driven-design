﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Domain.Common.Base;

public abstract class BaseEntity : AuditableEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class BaseEntity<TId> : BaseEntity
{
    public required TId Id { get; init; }
}