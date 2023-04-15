namespace DDD.Domain.Products;

public class Product : BaseEntity<ProductId>, IAggregateRoot
{
    public string Name { get; private init; } = string.Empty;

    public required Money Price { get; init; }

    public required Sku Sku { get; init; }

    private Product() { }

    public static Product Create(string name, Money price, Sku sku)
    {
        var product = new Product
        {
            Id = new ProductId(Guid.NewGuid()),
            Name = name,
            Price = price,
            Sku = sku
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }
}

public record ProductId(Guid Value);
