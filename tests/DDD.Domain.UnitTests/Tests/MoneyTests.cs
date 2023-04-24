using DDD.Domain.Common;
using FluentAssertions;

namespace DDD.Domain.UnitTests.Tests;

public class MoneyTests
{
    [Fact]
    public void Create_Should_Succeed_When_Money_Is_Valid()
    {
        // Arrange
        var currency = "USD";
        var amount = 100;
        // Act
        var money = new Money(currency, amount);
        // Assert
        money.Should().NotBeNull();
        money.Currency.Should().Be(currency);
        money.Amount.Should().Be(amount);
    }

    [Fact]
    public void Default_Should_Have_Zero_Amount_When_Created()
    {
        // Act
        var money = Money.Default;
        // Assert
        money.Should().NotBeNull();
        money.Currency.Should().Be("AUD");
        money.Amount.Should().Be(0);
    }

    [Fact]
    public void Default_Should_Have_Aud_Currency_When_Created()
    {
        // Act
        var money = Money.Default;
        // Assert
        money.Should().NotBeNull();
        money.Currency.Should().Be("AUD");
        money.Amount.Should().Be(0);
    }

    [Fact]
    public void Add_Should_Be_Correct_When_Two_Monies_Added()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var money3 = money1 + money2;
        // Assert
        money3.Should().NotBeNull();
        money3.Currency.Should().Be("AUD");
        money3.Amount.Should().Be(300);
    }

    [Fact]
    public void Subtract_Should_Be_Correct_When_Two_Monies_Subtracted()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var money3 = money1 - money2;
        // Assert
        money3.Should().NotBeNull();
        money3.Currency.Should().Be("AUD");
        money3.Amount.Should().Be(-100);
    }

    [Fact]
    public void LessThan_Should_Be_Correct_When_Two_Monies_Compared()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var result = money1 < money2;
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void LessThanOrEqual_Should_Be_Correct_When_Two_Monies_Compared()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var result = money1 <= money2;
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GreaterThan_Should_Be_Correct_When_Two_Monies_Compared()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var result = money1 > money2;
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GreaterThanOrEqual_Should_Be_Correct_When_Two_Monies_Compared()
    {
        // Arrange
        var money1 = new Money("AUD", 100);
        var money2 = new Money("AUD", 200);
        // Act
        var result = money1 >= money2;
        // Assert
        result.Should().BeFalse();
    }
}
