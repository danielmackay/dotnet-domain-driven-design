using DDD.Domain.Categories;
using DDD.Domain.Common.Exceptions;

namespace DDD.Domain.UnitTests.Tests;

public class CategoryTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Create_Should_Succeed_When_Category_Is_Valid()
    {
        // Arrange
        var name = _faker.Commerce.Categories(1)[0];
        var service = Substitute.For<ICategoryService>();

        // Act
        var category = Category.Create(name, service);

        // Assert
        category.Should().NotBeNull();
        category.Id.Should().NotBeNull();
        category.Name.Should().Be(name);
    }

    [Fact]
    public void Create_Should_Fail_When_Category_Exists()
    {
        // Arrange
        var name = _faker.Commerce.Categories(1)[0];
        var service = Substitute.For<ICategoryService>();
        service.CategoryExists(Any<string>()).Returns(true);

        // Act
        Action act = () => Category.Create(name, service);

        // Assert
        act.Should().Throw<DomainException>().WithMessage($"Category {name} already exists");
    }
}