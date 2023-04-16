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
}
