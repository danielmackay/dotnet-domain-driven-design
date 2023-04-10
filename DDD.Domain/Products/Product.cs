using DDD.Domain.Common;
using DDD.Domain.Interfaces;

namespace DDD.Domain.Products;

public class Product : IAggregateRoot
{
    public Guid Id { get; private init; }

    public string Name { get; private init; } = string.Empty;

    public required Money Price { get; init; }

    public required Sku Sku { get; init; }

    private Product()
    {
    }

    public static Product? Create(string name, Money price, Sku sku) => new Product
    {
        Id = Guid.NewGuid(),
        Name = name,
        Price = price,
        Sku = sku
    };
}
