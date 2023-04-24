using DDD.Domain.Common;

namespace DDD.Application.Orders.Queries.GetAllOrders;

public record OrderDto(Guid Id, CustomerDto Customer, Money Total);

public record CustomerDto(Guid Id, string FirstName, string LastName);