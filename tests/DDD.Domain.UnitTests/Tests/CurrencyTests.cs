using DDD.Domain.Common.Entities;
using DDD.Domain.Common.Exceptions;

namespace DDD.Domain.UnitTests.Tests;

public class CurrencyTests
{
    [Theory]
    [InlineData("AUD")]
    [InlineData("USD")]
    [InlineData("NZD")]
    [InlineData("GBP")]
    [InlineData("EUR")]
    [InlineData("CAD")]
    [InlineData("aud")]
    [InlineData("usd")]
    [InlineData("nzd")]
    [InlineData("gBp")]
    [InlineData("euR")]
    [InlineData("Cad")]
    public void Constructor_Should_Succeed_When_Currency_Is_Valid(string symbol)
    {
        // Act
        _ = new Currency(symbol);
    }

    [Theory]
    [InlineData("AUD1")]
    [InlineData("USD1")]
    [InlineData("NZD1")]
    [InlineData("abc")]
    [InlineData("123")]
    [InlineData("AUD ")]
    [InlineData(" USD")]
    public void Constructor_Should_Fail_When_Currency_Is_Invalid(string symbol)
    {
        // Act
        Action act = () => _ = new Currency(symbol);

        // Assert
        act.Should().Throw<DomainException>();
    }
}
