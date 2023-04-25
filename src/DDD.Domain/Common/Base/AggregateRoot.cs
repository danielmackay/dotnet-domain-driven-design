using DDD.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Domain.Common.Base;

public abstract class AggregateRoot<TId> : Entity<TId>, IDomainEvents
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyList<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
