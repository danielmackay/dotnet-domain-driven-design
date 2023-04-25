namespace DDD.Domain.Common.Interfaces;

public interface IDomainEvents
{
    IReadOnlyList<BaseEvent> DomainEvents { get; }

    void AddDomainEvent(BaseEvent domainEvent);

    void RemoveDomainEvent(BaseEvent domainEvent);

    void ClearDomainEvents();
}
