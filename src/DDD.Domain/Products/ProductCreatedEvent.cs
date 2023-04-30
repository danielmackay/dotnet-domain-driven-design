namespace DDD.Domain.Products;

public record ProductCreatedEvent(ProductId Product, string ProductName) : DomainEvent
{
    public static ProductCreatedEvent Create(Product product) => new(product.Id, product.Name);
}