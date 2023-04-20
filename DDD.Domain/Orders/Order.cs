using DDD.Domain.Customers;
using DDD.Domain.DomainServices;
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

    public DateTimeOffset ShippingDate { get; private set; }

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

        // NOTE: this is currently firing an event every time an order is loading from the DB 😢
        AddDomainEvent(new OrderCreatedEvent(this));
    }

    public LineItem AddLineItem(ProductId productId, Money price, int quantity)
    {
        DomainException.ThrowIf(Status != OrderStatus.PendingPayment, "Can't modify order once payment is done");

        var lineItem = LineItem.Create(Id, productId, price, quantity);

        var first = _lineItems.FirstOrDefault();
        if (first != null && first.Price.Currency != lineItem.Price.Currency)
            throw new DomainException($"Cannot add line item with currency {lineItem.Price.Currency} to and order than already contains a currency of {first.Price.Currency}");

        lineItem.AddDomainEvent(new LineItemCreatedEvent(lineItem));

        _lineItems.Add(lineItem);

        return lineItem;
    }

    public void RemoveLineItem(ProductId productId)
    {
        DomainException.ThrowIf(Status != OrderStatus.PendingPayment, "Can't modify order once payment is done");

        var lineItem = _lineItems.RemoveAll(x => x.ProductId == productId);
    }

    public void AddPayment(Money payment)
    {
        DomainException.ThrowIf(payment.Amount <= 0, "Payments can't be negative");
        DomainException.ThrowIf(payment > OrderTotal - AmountPaid, "Payment can't exceed order total");

        AmountPaid += payment;

        if (AmountPaid >= OrderTotal)
        {
            Status = OrderStatus.ReadyForShipping;
            AddDomainEvent(new OrderReadyForShippingEvent(this));
        }
    }

    public void AddQuantity(ProductId productId, int quantity) =>
        _lineItems.FirstOrDefault(li => li.ProductId == productId)?.AddQuantity(quantity);

    public void RemoveQuantity(ProductId productId, int quantity) =>
        _lineItems.FirstOrDefault(li => li.ProductId == productId)?.RemoveQuantity(quantity);

    public void ShipOrder(IDateTime dateTime)
    {
        DomainException.ThrowIf(_lineItems.Sum(li => li.Quantity) <= 0, "Can't ship an order with no items");
        DomainException.ThrowIf(Status == OrderStatus.PendingPayment, "Can't ship an unpaid order");
        DomainException.ThrowIf(Status == OrderStatus.InTransit, "Order already shipped to customer");

        ShippingDate = dateTime.Now;
        Status = OrderStatus.InTransit;
    }
}
