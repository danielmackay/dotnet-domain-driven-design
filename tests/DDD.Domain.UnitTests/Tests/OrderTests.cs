using DDD.Domain.Common.Entities;
using DDD.Domain.Common.Exceptions;
using DDD.Domain.Customers;
using DDD.Domain.DomainServices;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using DDD.Domain.UnitTests.Fakers;

namespace DDD.Domain.UnitTests.Tests;

public class OrderTests
{
    private readonly Faker<Money> _moneyFaker = MoneyFaker.Create();

    [Fact]
    public void Create_Should_Succeed_When_Customer_Is_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());

        // Act
        var order = Order.Create(customerId);
        // Assert
        order.Should().NotBeNull();
        order.CustomerId.Should().Be(customerId);
    }

    [Fact]
    public void AddLineItem_Should_Add_Line_Item_When_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        // Act
        var lineItem = order.AddLineItem(productId, price, quantity);
        // Assert
        lineItem.Should().NotBeNull();
        order.Should().NotBeNull();
        order.LineItems.Should().NotBeEmpty();
        order.LineItems.Should().ContainSingle();
        order.LineItems.Should().ContainSingle(x => x.Price == price);
        order.LineItems.Should().ContainSingle(x => x.Quantity == quantity);
    }

    [Fact]
    public void AddLineItem_Should_Add_Domain_Event_When_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        // Act
        var lineItem = order.AddLineItem(productId, price, quantity);
        // Assert
        order.DomainEvents.Should().NotBeEmpty();
        order.DomainEvents.Should().ContainSingle(x => x is LineItemCreatedEvent);
    }

    [Fact]
    public void AddLineItem_Should_Throw_When_Quantity_Is_Zero()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 0;
        // Act
        Action act = () => order.AddLineItem(productId, price, quantity);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void AddLineItem_Should_Throw_When_Quantity_Is_Negative()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = -1;
        // Act
        Action act = () => order.AddLineItem(productId, price, quantity);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void AddLineItem_Should_Throw_When_Price_Is_Zero()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = new Money("USD", 0);
        var quantity = 1;
        // Act
        Action act = () => order.AddLineItem(productId, price, quantity);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void AddLineItem_Should_Throw_When_Price_Is_Negative()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = new Money("USD", -1);
        var quantity = 1;
        // Act
        Action act = () => order.AddLineItem(productId, price, quantity);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void RemoveLineItem_Should_Remove_Item_When_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        order.AddLineItem(productId, price, quantity);
        // Act
        order.RemoveLineItem(productId);
        // Assert
        order.LineItems.Should().BeEmpty();
    }

    [Fact]
    public void RemoveLineItem_Should_Throw_When_Status_Is_Not_Pending()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        var lineItem = order.AddLineItem(productId, price, quantity);
        order.AddPayment(price);
        // Act
        Action act = () => order.RemoveLineItem(productId);
        // Assert
        act.Should().Throw<ConditionDomainException>().WithMessage("Can't modify order once payment is done");
    }

    [Fact]
    public void AddPayment_Should_Add_Payment_When_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var productId = new ProductId(Guid.NewGuid());
        var quantity = 1;
        order.AddLineItem(productId, price, quantity);
        // Act
        order.AddPayment(price);
        // Assert
        order.Should().NotBeNull();
        order.AmountPaid.Should().Be(price);
    }

    [Fact]
    public void AddPayment_Should_Add_Domain_Event_When_Full_Amount_Paid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var productId = new ProductId(Guid.NewGuid());
        var quantity = 1;
        order.AddLineItem(productId, price, quantity);
        // Act
        order.AddPayment(price);
        // Assert
        order.DomainEvents.Should().NotBeEmpty();
        order.DomainEvents.Should().ContainSingle(x => x is OrderReadyForShippingEvent);
    }

    [Fact]
    public void AddPayment_Should_Throw_When_Amount_Is_Zero()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = new Money("USD", 0);
        // Act
        Action act = () => order.AddPayment(price);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void AddPayment_Should_Throw_When_Amount_Is_Negative()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = new Money("USD", -1);
        // Act
        Action act = () => order.AddPayment(price);
        // Assert
        act.Should().Throw<ZeroOrNegativeDomainException>();
    }

    [Fact]
    public void AddPayment_Should_Throw_When_Total_Payments_Are_Greater_Than_Total()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var productId = new ProductId(Guid.NewGuid());
        var quantity = 1;
        order.AddLineItem(productId, price, quantity);
        order.AddPayment(price);
        // Act
        Action act = () => order.AddPayment(price);
        // Assert
        act.Should().Throw<ConditionDomainException>().WithMessage("Payment can't exceed order total");
    }

    [Fact]
    public void AddPayment_Should_Set_Status_To_ReadyForShipping_When_Full_Amount_Paid()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var price = _moneyFaker.Generate();
        order.AddLineItem(new ProductId(Guid.NewGuid()), price, 1);
        // Act
        order.AddPayment(price);
        // Assert
        order.Status.Should().Be(OrderStatus.ReadyForShipping);
    }

    [Fact]
    public void AddQuantity_Should_Increment_Quantity_By_1()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        var lineItem = order.AddLineItem(productId, price, quantity);
        // Act
        order.AddQuantity(productId, quantity);
        // Assert
        order.LineItems.Should().ContainSingle();
        order.LineItems.Should().ContainSingle(x => x.Quantity == 2);
    }

    [Fact]
    public void RemoveQuantity_Should_Decrement_Quantity_By_1()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantityToAdd = 2;
        var quantityToRemove = 1;
        var lineItem = order.AddLineItem(productId, price, quantityToAdd);
        // Act
        order.RemoveQuantity(productId, quantityToRemove);
        // Assert
        order.LineItems.Should().ContainSingle();
        order.LineItems.Should().ContainSingle(x => x.Quantity == 1);
    }

    [Fact]
    public void ShipOrder_Should_Throw_When_There_Are_No_Items()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var dateTime = Substitute.For<IDateTime>();
        // Act
        Action act = () => order.ShipOrder(dateTime);
        // Assert
        act.Should().Throw<ConditionDomainException>().WithMessage("Can't ship an order with no items");

    }

    [Fact]
    public void ShipOrder_Should_Throw_When_Status_Is_Not_ReadyForShipping()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var productPrice = new Money("AUD", 20);
        order.AddLineItem(new ProductId(Guid.NewGuid()), productPrice, 1);
        var dateTime = Substitute.For<IDateTime>();
        var paymentPrice = new Money("AUD", 10);
        order.AddPayment(paymentPrice);
        // Act
        Action act = () => order.ShipOrder(dateTime);
        // Assert
        act.Should().Throw<ConditionDomainException>().WithMessage("Can't ship an unpaid order");
    }

    [Fact]
    public void ShipOrder_Should_Set_Status_To_Shipped()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        order.AddLineItem(new ProductId(Guid.NewGuid()), price, 1);
        var dateTime = Substitute.For<IDateTime>();
        order.AddPayment(price);
        // Act
        order.ShipOrder(dateTime);
        // Assert
        order.Status.Should().Be(OrderStatus.InTransit);
    }

    [Fact]
    public void ShipOrder_Should_Update_ShippingDate_When_Valid()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        order.AddLineItem(new ProductId(Guid.NewGuid()), price, 1);
        var now = DateTime.UtcNow;
        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(now);
        order.AddPayment(price);
        // Act
        order.ShipOrder(dateTime);
        // Assert
        order.ShippingDate.Should().Be(now);
    }

    [Fact]
    public void AddLineItem_Should_Update_Quantity_When_ProductExists()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());
        var order = Order.Create(customerId);
        var price = _moneyFaker.Generate();
        var quantity = 1;
        order.AddLineItem(productId, price, quantity);
        // Act
        order.AddLineItem(productId, price, quantity);
        // Assert
        order.LineItems.Should().ContainSingle();
        order.LineItems.Should().ContainSingle(x => x.Quantity == 2);
    }
}