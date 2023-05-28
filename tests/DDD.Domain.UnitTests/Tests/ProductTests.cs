using DDD.Domain.Categories;
using DDD.Domain.Common.Entities;
using DDD.Domain.Common.Exceptions;
using DDD.Domain.Products;
using DDD.Domain.UnitTests.Fakers;

namespace DDD.Domain.UnitTests.Tests;

public class ProductTests
{
    private readonly Faker _faker = new();
    private readonly Faker<Money> _moneyFaker = MoneyFaker.Create();
    private readonly Faker<Sku> _skuFaker = SkuFaker.Create();

    [Fact]
    public void Create_Should_Succeed_When_Product_Is_Valid()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        var price = _moneyFaker.Generate();
        var sku = _skuFaker.Generate();
        var categoryId = new CategoryId(Guid.NewGuid());
        // Act
        var product = Product.Create(name, price, sku, categoryId);
        // Assert
        product.Should().NotBeNull();
        product.Id.Should().NotBeNull();
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
    }

    [Fact]
    public void Create_Should_Add_Domain_Event_When_Customer_Is_Valid()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        var price = _moneyFaker.Generate();
        var sku = _skuFaker.Generate();
        var categoryId = new CategoryId(Guid.NewGuid());
        // Act
        var product = Product.Create(name, price, sku, categoryId);
        // Assert
        product.DomainEvents.Should().NotBeEmpty();
        product.DomainEvents.Should().ContainSingle();
        product.DomainEvents.Should().ContainSingle(x => x is ProductCreatedEvent);
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Empty()
    {
        // Arrange
        var name = string.Empty;
        var price = _moneyFaker.Generate();
        var sku = _skuFaker.Generate();
        var categoryId = new CategoryId(Guid.NewGuid());

        // Act
        Action act = () => Product.Create(name, price, sku, categoryId);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("name cannot be empty");
    }

    [Fact]
    public void Create_Should_Throw_When_Price_Is_Null()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        Money? price = null;
        var sku = _skuFaker.Generate();
        var categoryId = new CategoryId(Guid.NewGuid());

        // Act
        Action act = () => Product.Create(name, price!, sku, categoryId);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("price cannot be null");
    }

    [Fact]
    public void Create_Should_Throw_When_Sku_Is_Null()
    {
        // Arrange
        var name = _faker.Commerce.ProductName();
        var price = _moneyFaker.Generate();
        Sku? sku = null;
        var categoryId = new CategoryId(Guid.NewGuid());

        // Act
        Action act = () => Product.Create(name, price, sku!, categoryId);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("sku cannot be null");
    }

    [Fact]
    public void UpdatePrice_Should_Succeed_When_Price_Is_Valid()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        var newPrice = _moneyFaker.Generate();
        // Act
        product.UpdatePrice(newPrice);
        // Assert
        product.Price.Should().Be(newPrice);
    }

    [Fact]
    public void UpdatePrice_Should_Throw_When_Price_Is_Null()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        Money? newPrice = null;
        // Act
        Action act = () => product.UpdatePrice(newPrice!);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("price cannot be null");
    }

    [Fact]
    public void UpdateName_Should_Succeed_When_Name_Is_Valid()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        var newName = _faker.Commerce.ProductName();
        // Act
        product.UpdateName(newName);
        // Assert
        product.Name.Should().Be(newName);
    }

    [Fact]
    public void UpdateName_Should_Throw_When_Name_Is_Empty()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        var newName = string.Empty;
        // Act
        Action act = () => product.UpdateName(newName);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("name cannot be empty");
    }

    [Fact]
    public void UpdateSku_Should_Succeed_When_Sku_Is_Valid()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        var newSku = _skuFaker.Generate();
        // Act
        product.UpdateSku(newSku);
        // Assert
        product.Sku.Should().Be(newSku);
    }

    [Fact]
    public void UpdateSku_Should_Throw_When_Sku_Is_Null()
    {
        // Arrange
        var categoryId = new CategoryId(Guid.NewGuid());
        var product = Product.Create(_faker.Commerce.ProductName(), _moneyFaker.Generate(), _skuFaker.Generate(), categoryId);
        Sku? newSku = null;
        // Act
        Action act = () => product.UpdateSku(newSku!);
        // Assert
        act.Should().Throw<DomainException>().WithMessage("sku cannot be null");
    }
}
