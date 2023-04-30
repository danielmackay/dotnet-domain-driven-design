using DDD.Domain.Customers;
using Newtonsoft.Json;

namespace DDD.Domain.UnitTests.Tests;

public class OutboxMessages
{
    [Fact]
    public void Can_Serialize_Data()
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
        result.Should().Be(customer.Email);
    }
}
