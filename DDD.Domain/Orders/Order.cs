using DDD.Domain.Common;
using DDD.Domain.Customers;
using DDD.Domain.Interfaces;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order : IAggregateRoot
{
    // Ensures lined items are unique
    private readonly HashSet<LineItem> _lineItems = new();

    public required OrderId Id { get; init; }

    public required CustomerId CustomerId { get; init; }

    private Order() { }

    public static Order? Create(CustomerId customerId) => new()
    {
        Id = new OrderId(Guid.NewGuid()),
        CustomerId = customerId,
    };

    public void AddLineItem(ProductId productId, Money price)
    {
        var lineItem = new LineItem(Id, productId, price);
        _lineItems.Add(lineItem);
    }
}

public record OrderId(Guid Value);

