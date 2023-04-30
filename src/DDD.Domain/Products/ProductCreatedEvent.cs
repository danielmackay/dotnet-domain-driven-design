namespace DDD.Domain.Products;

public record ProductCreatedEvent(ProductId Product, string ProductName) : DomainEvent
{
    public ProductCreatedEvent(Product product) : this(product.Id, product.Name) { }
}