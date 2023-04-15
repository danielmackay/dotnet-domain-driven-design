using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class LineItem : BaseEntity<LineItemId>, IEntity
{
    public required OrderId OrderId { get; init; }

    public required ProductId ProductId { get; init; }

    // Detatch price from product to capture the price at the time of purchase
    public required Money Price { get; init; }

    private LineItem() { }

    // Internal so that only the Order can create a LineItem
    internal static LineItem Create(OrderId orderId, ProductId productId, Money price) => new()
    {
        Id = new LineItemId(Guid.NewGuid()),
        OrderId = orderId,
        ProductId = productId,
        Price = price
    };
}

public record LineItemId(Guid Value);
