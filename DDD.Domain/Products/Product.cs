namespace DDD.Domain.Products;

public class Product : BaseEntity<ProductId>, IAggregateRoot
{
    public required string Name { get; init; }

    public required Money Price { get; init; }

    public required Sku Sku { get; init; }

    private Product() : base(new ProductId(Guid.NewGuid())) { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Product Create(string name, Money price, Sku sku)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            Sku = sku
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }
}
