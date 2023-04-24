namespace DDD.Domain.DomainServices;

public interface IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
