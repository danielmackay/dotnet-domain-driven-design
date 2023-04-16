using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class LineItem : BaseEntity<LineItemId>, IEntity
{
    public required OrderId OrderId { get; init; }

    public required ProductId ProductId { get; init; }

    public Product? Product { get; init; }

    // Detatch price from product to capture the price at the time of purchase
    public required Money Price { get; init; }

    // Internal so that only the Order can create a LineItem
    private LineItem() : base(new LineItemId(Guid.NewGuid())) { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money) be passed via the constructor
    internal static LineItem Create(OrderId orderId, ProductId productId, Money price)
    {
        var lineItem = new LineItem()
        {
            OrderId = orderId,
            ProductId = productId,
            Price = price
        };

        return lineItem;
    }
}

