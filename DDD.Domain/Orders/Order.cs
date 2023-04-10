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
