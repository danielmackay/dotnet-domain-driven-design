using DDD.Domain.Common.Interfaces;

namespace DDD.Domain.Common.Base;

public abstract class Entity<TId> : IAuditableEntity
{
    public required TId Id { get; init; }
    public DateTimeOffset CreatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public void Created(DateTimeOffset dateTime, string? user)
    {
        CreatedAt = dateTime;
        CreatedBy = user;
    }

    public void Updated(DateTimeOffset dateTime, string? user)
    {
        UpdatedAt = dateTime;
        UpdatedBy = user;
    }
}
