using DDD.Domain.Common.Extensions;

namespace DDD.Domain.Products;

public class Product : BaseEntity<ProductId>, IAggregateRoot
{
    public string Name { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public Sku Sku { get; private set; } = null!;

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

    public void UpdateName(string name)
    {
        DomainException.ThrowIf(name.IsEmpty(), "Name cannot be empty");
        Name = name;
    }

    public void UpdatePrice(Money price)
    {
        DomainException.ThrowIf(price.Amount <= 0, "Price must be positive");
        Price = price;
    }

    public void UpdateSku(Sku sku)
    {
        Sku = sku;
    }
}
