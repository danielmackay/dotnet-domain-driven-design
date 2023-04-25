namespace DDD.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public Sku Sku { get; private set; } = null!;

    private Product() { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Product Create(string name, Money price, Sku sku)
    {
        Guard.Against.Empty(name);
        Guard.Against.Null(sku);
        Guard.Against.Null(price);
        Guard.Against.ZeroOrNegative(price.Amount);

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

    public void UpdateName(string name)
    {
        Guard.Against.Empty(name);
        Name = name;
    }

    public void UpdatePrice(Money price)
    {
        Guard.Against.Null(price);
        Guard.Against.ZeroOrNegative(price.Amount);
        Price = price;
    }

    public void UpdateSku(Sku sku)
    {
        Guard.Against.Null(sku);
        Sku = sku;
    }
}
