using DDD.Domain.Products;


namespace DDD.Domain.UnitTests.Tests;

public class SkuTests
{
    [Fact]
    public void Sku_Create_WithNullValue_ReturnsNull()
    {
        // Arrange
        var value = null as string;
        // Act
        var result = Sku.Create(value!);
        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Sku_Create_WithEmptyValue_ReturnsNull()
    {
        // Arrange
        var value = string.Empty;
        // Act
        var result = Sku.Create(value);
        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Sku_Create_WithWhiteSpaceValue_ReturnsNull()
    {
        // Arrange
        var value = " ";
        // Act
        var result = Sku.Create(value);
        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Sku_Create_WithInvalidLengthValue_ReturnsNull()
    {
        // Arrange
        var value = "123456789";
        // Act
        var result = Sku.Create(value);
        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Sku_Create_WithValidValue_ReturnsSku()
    {
        // Arrange
        var value = "12345678";
        // Act
        var result = Sku.Create(value);
        // Assert
        result.Should().NotBeNull();
        result!.Value.Should().Be(value);
    }

    [Fact]
    public void Sku_Create_WithValidValue_ReturnsSameInstance()
    {
        // Arrange
        var value = "12345678";
        // Act
        var result1 = Sku.Create(value);
        var result2 = Sku.Create(value);
        // Assert
        result1.Should().BeEquivalentTo(result2);
    }

    [Fact]
    public void Sku_Create_WithValidValue_ReturnsDifferentInstance()
    {
        // Arrange
        var value1 = "12345678";
        var value2 = "87654321";
        // Act
        var result1 = Sku.Create(value1);
        var result2 = Sku.Create(value2);
        // Assert
        result1.Should().NotBeEquivalentTo(result2);
    }
}
