﻿namespace DDD.Domain.Common;

public record Money(string Currency, decimal Amount) : IValueObject
{
    public static Money Default => new("AUD", 0);

    public static Money Zero => Default;

    public static Money operator +(Money left, Money right) => new Money(left.Currency, left.Amount + right.Amount);

    public static Money operator -(Money left, Money right) => new Money(left.Currency, left.Amount - right.Amount);

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;
}