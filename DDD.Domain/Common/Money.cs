namespace DDD.Domain.Common;

public record Money(string Currency, decimal Amount) : IValueObject
{
    public static Money Default => new("N/A", 0);
}
