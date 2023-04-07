using DDD.Domain.Interfaces;

namespace DDD.Domain.Common;

public record Money(string Currency, decimal Amount) : IValueObject;
