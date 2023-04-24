using DDD.Domain.Common.Extensions;
using System.Runtime.CompilerServices;

namespace DDD.Domain.Common.Exceptions;

public class DomainException : Exception
{
    public DomainException()
        : base()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIf(bool condition)
    {
        if (condition)
            throw new DomainException();
    }

    public static void ThrowIf(bool condition, string message)
    {
        if (condition)
            throw new DomainException(message);
    }

    public static void ThrowIfEmpty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty())
            throw new DomainException($"{paramName} cannot be empty");
    }
}