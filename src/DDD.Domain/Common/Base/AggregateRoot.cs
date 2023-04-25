using DDD.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Domain.Common.Base;

public abstract class AggregateRoot<TId> : Entity<TId>, IDomainEvents
{
    private readonly List<DomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
