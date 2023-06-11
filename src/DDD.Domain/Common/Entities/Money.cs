namespace DDD.Domain.Common.Entities;

public record Money(Currency Currency, decimal Amount)
{
    public static Money Default => new(Currency.Default, 0);

    public static Money Zero => Default;

    public static Money operator +(Money left, Money right) => new Money(left.Currency, left.Amount + right.Amount);

    public static Money operator -(Money left, Money right) => new Money(left.Currency, left.Amount - right.Amount);

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;
}
