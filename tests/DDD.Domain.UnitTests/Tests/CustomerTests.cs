using DDD.Domain.Common.Exceptions;
using DDD.Domain.Customers;

namespace DDD.Domain.UnitTests.Tests;

public class CustomerTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void Create_Should_Succeed_When_Customer_Is_Valid()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();

        // Act
        var customer = Customer.Create(email, firstName, lastName);

        // Assert
        customer.Should().NotBeNull();
        customer.Id.Should().NotBeNull();
        customer.Email.Should().Be(email);
        customer.FirstName.Should().Be(firstName);
        customer.LastName.Should().Be(lastName);
    }

    [Fact]
    public void Create_Should_Throw_When_Email_Is_Empty()
    {
        // Arrange
        var email = string.Empty;
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();

        // Act
        Action act = () => Customer.Create(email, firstName, lastName);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("email cannot be empty");
    }

    [Fact]
    public void Create_Should_Throw_When_FirstName_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = string.Empty;
        var lastName = _faker.Name.LastName();

        // Act
        Action act = () => Customer.Create(email, firstName, lastName);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("firstName cannot be empty");
    }

    [Fact]
    public void Create_Should_Throw_When_LastName_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = string.Empty;

        // Act
        Action act = () => Customer.Create(email, firstName, lastName);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("lastName cannot be empty");
    }

    [Fact]
    public void UpdateName_Should_Succeed_When_Customer_Is_Valid()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var newFirstName = _faker.Name.FirstName();
        var newLastName = _faker.Name.LastName();

        // Act
        customer.UpdateName(newFirstName, newLastName);

        // Assert
        customer.FirstName.Should().Be(newFirstName);
        customer.LastName.Should().Be(newLastName);
    }

    [Fact]
    public void UpdateName_Should_Throw_When_FirstName_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var newFirstName = string.Empty;
        var newLastName = _faker.Name.LastName();

        // Act
        Action act = () => customer.UpdateName(newFirstName, newLastName);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("firstName cannot be empty");
    }

    [Fact]
    public void UpdateName_Should_Throw_When_LastName_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var newFirstName = _faker.Name.FirstName();
        var newLastName = string.Empty;

        // Act
        Action act = () => customer.UpdateName(newFirstName, newLastName);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("lastName cannot be empty");
    }

    [Fact]
    public void UpdateAddress_Should_Succeed_When_Customer_Is_Valid()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var address = _faker.Address.StreetAddress();

        // Act
        customer.UpdateAddress(address);

        // Assert
        customer.Address.Should().Be(address);
    }

    [Fact]
    public void UpdateAddress_Should_Throw_When_Address_Is_Empty()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        var customer = Customer.Create(email, firstName, lastName);
        var address = string.Empty;

        // Act
        Action act = () => customer.UpdateAddress(address);

        // Assert
        act.Should().Throw<DomainException>().WithMessage("address cannot be empty");
    }

    [Fact]
    public void Create_Should_Add_Domain_Event_When_Customer_Is_Valid()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();

        // Act
        var customer = Customer.Create(email, firstName, lastName);

        // Assert
        customer.DomainEvents.Should().NotBeEmpty();
        customer.DomainEvents.Should().ContainSingle();
        customer.DomainEvents.Should().ContainSingle(x => x is CustomerCreatedEvent);
    }
}