using DDD.Domain.Customers;
using Newtonsoft.Json;

namespace DDD.Domain.UnitTests.Tests;

public class OutboxMessages
{
    [Fact]
    public void Customer_Created_Event_Should_Be_Deserialized_Successfully()
    {
        // Arrange
        var customer = Customer.Create("email@gmail.com", "firstName", "lastName");
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        var customerCreatedEvent = customer.DomainEvents[0];

        var json = JsonConvert.SerializeObject(customerCreatedEvent, settings);

        // Act
        var result = JsonConvert.DeserializeObject<CustomerCreatedEvent>(json);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CustomerCreatedEvent>();
        result.Should().NotBeNull();
        result!.FirstName.Should().Be(customer.FirstName);
        result.LastName.Should().Be(customer.LastName);
        result.Id.Should().Be(customer.Id);
    }
}
