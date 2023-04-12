using DDD.Domain.Common;
using DDD.Domain.Interfaces;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class LineItem : IValueObject
{
    public LineItemId Id { get; }

    public OrderId OrderId { get; }

    public ProductId ProductId { get; }

    // Detatch price from product to capture the price at the time of purchase
    public Money Price { get; }

    // Internal so that only the Order can create a LineItem
    internal LineItem(OrderId orderId, ProductId productId, Money price)
    {
        Id = new LineItemId(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }
}

public record LineItemId(Guid Value);
