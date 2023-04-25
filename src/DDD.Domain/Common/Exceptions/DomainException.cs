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
}

public class EmptyDomainException : DomainException
{
    public EmptyDomainException(string msg) : base(msg) { }
}

public class NullDomainException : DomainException
{
    public NullDomainException(string msg) : base(msg) { }
}

public class ZeroOrNegativeDomainException : DomainException
{
    public ZeroOrNegativeDomainException(string msg) : base(msg) { }
}

public class ConditionDomainException : DomainException
{
    public ConditionDomainException(string msg) : base(msg) { }
}
