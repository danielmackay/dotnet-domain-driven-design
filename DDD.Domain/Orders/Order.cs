using DDD.Domain.Common.Exceptions;
using DDD.Domain.Customers;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order : BaseEntity<OrderId>, IAggregateRoot
{
    private readonly List<LineItem> _lineItems = new();

    public IEnumerable<LineItem> LineItems => _lineItems.ToList();

    public required CustomerId CustomerId { get; init; }

    public Customer? Customer { get; set; }

    public Money Total
    {
        get
        {
            if (_lineItems.Count == 0)
                return Money.Default;

            var amount = _lineItems.Sum(li => li.Price.Amount);
            var currency = _lineItems[0].Price.Currency;

            return new Money(currency, amount);
        }
    }

    private Order() { }

    public static Order Create(CustomerId customerId)
    {
        var order = new Order()
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public LineItem AddLineItem(ProductId productId, Money price)
    {
        var lineItem = LineItem.Create(Id, productId, price);

        var first = _lineItems.FirstOrDefault();
        if (first != null && first.Price.Currency != lineItem.Price.Currency)
            throw new DomainException($"Cannot add line item with currency {lineItem.Price.Currency} to and order than already contains a currency of {first.Price.Currency}");

        lineItem.AddDomainEvent(new LineItemCreatedEvent(lineItem));

        _lineItems.Add(lineItem);

        return lineItem;
    }
}

public record OrderId(Guid Value);

