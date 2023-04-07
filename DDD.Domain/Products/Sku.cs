using DDD.Domain.Interfaces;

namespace DDD.Domain.Products;

public record Sku : IValueObject
{
    private const int DefaultLength = 15;

    private string Value { get; }

    private Sku(string value) => Value = value;

    public static Sku? Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        if (value.Length != DefaultLength)
            return null;

        return new Sku(value);
    }
}
