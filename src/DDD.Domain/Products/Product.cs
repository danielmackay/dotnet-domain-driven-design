using DDD.Domain.Categories;
using DDD.Domain.Common.Entities;

namespace DDD.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public CategoryId CategoryId { get; set; } = null!;

    public string Name { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public Sku Sku { get; private set; } = null!;

    private Product() { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Product Create(string name, Money price, Sku sku, CategoryId categoryId)
    {
        Guard.Against.Empty(name);
        Guard.Against.Null(sku);
        Guard.Against.Null(price);
        Guard.Against.ZeroOrNegative(price.Amount);
        Guard.Against.Null(categoryId);

        var product = new Product
        {
            Id = new ProductId(Guid.NewGuid()),
            CategoryId = categoryId,
            Name = name,
            Price = price,
            Sku = sku
        };

        product.AddDomainEvent(ProductCreatedEvent.Create(product));

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
