using DDD.Domain.Common.Exceptions;
using DDD.Domain.Customers;
using DDD.Domain.Products;

namespace DDD.Domain.Orders;

public class Order : BaseEntity<OrderId>, IAggregateRoot
{
    private readonly List<LineItem> _lineItems = new();

    public IEnumerable<LineItem> LineItems => _lineItems.ToList();

    public CustomerId CustomerId { get; }

    public Customer? Customer { get; set; }

    public Money AmountPaid { get; private set; }

    public OrderStatus Status { get; private set; }

    public Money OrderTotal
    {
        get
        {
            if (_lineItems.Count == 0)
                return Money.Default;

            var amount = _lineItems.Sum(li => li.Price.Amount * li.Quantity);
            var currency = _lineItems[0].Price.Currency;

            return new Money(currency, amount);
        }
    }

    public Order(CustomerId customerId)
        : base(new OrderId(Guid.NewGuid()))
    {
        CustomerId = customerId;
        AmountPaid = Money.Default;
        Status = OrderStatus.PendingPayment;
        AddDomainEvent(new OrderCreatedEvent(this));
    }

    public LineItem AddLineItem(ProductId productId, Money price, int quantity)
    {
        var lineItem = LineItem.Create(Id, productId, price, quantity);

        var first = _lineItems.FirstOrDefault();
        if (first != null && first.Price.Currency != lineItem.Price.Currency)
            throw new DomainException($"Cannot add line item with currency {lineItem.Price.Currency} to and order than already contains a currency of {first.Price.Currency}");

        lineItem.AddDomainEvent(new LineItemCreatedEvent(lineItem));

        _lineItems.Add(lineItem);

        return lineItem;
    }

    public void AddPayment(Money payment)
    {
        Guard.Against.NegativeOrZero(payment.Amount);
        Guard.Against.AgainstExpression(amount => amount > OrderTotal.Amount - AmountPaid.Amount, payment.Amount, "Payment can't exceed order total");

        AmountPaid += payment;

        if (AmountPaid >= OrderTotal)
        {
            Status = OrderStatus.ReadyForShipping;
            AddDomainEvent(new OrderReadyForShippingEvent(this));
        }
    }
}
