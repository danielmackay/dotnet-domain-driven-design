using DDD.Domain.Common;
using DDD.Domain.Customers;
using DDD.Domain.Interfaces;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order : IAggregateRoot
{
    // Ensures lined items are unique
    private readonly HashSet<LineItem> _lineItems = new();

    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    // TODO: Consider tidying up constructor (should we remove init setters?)
    private Order()
    {
    }

    public static Order? Create(Customer customer)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customer.Id,
        };

        return order;
    }

    // TODO: Pass in ProductId instead
    public void AddLineItem(Product product)
    {
        var lineItem = new LineItem(Guid.NewGuid(), Id, product.Id, product.Price);
        _lineItems.Add(lineItem);
    }
}

public class LineItem : IValueObject
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    // Detatch price from product to capture the price at the time of purchase
    public Money Price { get; private set; }

    // Internal so that only the Order can create a LineItem
    internal LineItem(Guid id, Guid orderId, Guid productId, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Price = price;
    }
}
