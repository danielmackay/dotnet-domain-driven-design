using DDD.Domain.Customers;
using DDD.Domain.Orders;
using System.Text.Json;

namespace DDD.Domain.UnitTests.Tests;

public class SerializationTests
{
    [Fact]
    public void Serialize_Should_Return_Json_String_For_Event()
    {
        // Arrange
        var order = Order.Create(new CustomerId(Guid.NewGuid()));
        var evt = new OrderCreatedEvent(order);

        // Act
        var json = JsonSerializer.Serialize(evt);

        // Assert

        json.Should().NotBeEmpty();
    }
}
