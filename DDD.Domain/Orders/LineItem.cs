using DDD.Domain.Common;
using DDD.Domain.Interfaces;

namespace DDD.Domain.Orders;

public class LineItem : IValueObject
{
    public Guid Id { get; }

    public Guid OrderId { get; }

    public Guid ProductId { get; }

    // Detatch price from product to capture the price at the time of purchase
    public Money Price { get; }

    // Internal so that only the Order can create a LineItem
    internal LineItem(Guid id, Guid orderId, Guid productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }
}
