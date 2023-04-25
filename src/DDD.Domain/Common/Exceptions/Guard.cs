using DDD.Domain.Common.Extensions;
using System.Runtime.CompilerServices;

namespace DDD.Domain.Common.Exceptions;

internal static class Guard
{
    internal static class Against
    {
        public static void Condition(bool condition, string message)
        {
            if (condition)
                throw new ConditionDomainException(message);
        }

        public static void Empty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value.IsEmpty())
                throw new EmptyDomainException($"{paramName} cannot be empty");
        }

        public static void ZeroOrNegative(decimal value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (value <= 0)
                throw new ZeroOrNegativeDomainException($"{paramName} cannot be zero or negative");
        }

        public static void Null<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null) where T : class
        {
            if (value is null)
                throw new NullDomainException($"{paramName} cannot be null");
        }
    }
}