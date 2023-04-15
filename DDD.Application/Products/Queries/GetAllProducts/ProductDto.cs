namespace DDD.Application.Products.Queries.GetAllProducts;

public record ProductDto(Guid Id, string Name, string Sku, decimal Amount, string Currency);
