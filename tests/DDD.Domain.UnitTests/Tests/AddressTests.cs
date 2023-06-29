using DDD.Domain.Common.Exceptions;
using DDD.Domain.Customers;

namespace DDD.Domain.UnitTests.Tests;

public class AddressTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void UpdateAddress_Should_Throw_When_Address_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var street = string.Empty;
        var city = string.Empty;
        var state = string.Empty;
        var country = string.Empty;
        var zipCode = string.Empty;

        // Act
        Action act = () => _ = new Address(street, null, city, state, zipCode, country);

        // Assert
        act.Should().Throw<EmptyDomainException>();
    }
}
