namespace DDD.Domain.Common;

public record Money(string Currency, decimal Amount) : IValueObject//, IComparable<Money>
{
    public static Money Default => new("AUD", 0);

    //public int CompareTo(Money? other)
    //{
    //    return other == null ? 1 : (int)(Amount - other.Amount);
    //}

    public static Money operator +(Money left, Money right) => new Money(left.Currency, left.Amount + right.Amount);

    public static Money operator -(Money left, Money right) => new Money(left.Currency, left.Amount - right.Amount);

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    //public static Money Add(Money left, Money right) => new Money(left.Currency, left.Amount + right.Amount);

    //public static Money Subtract(Money left, Money right) => new Money(left.Currency, left.Amount - right.Amount);
}