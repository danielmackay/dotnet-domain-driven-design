namespace DDD.Application.Features.Customers.Queries.GetAllCustomers;

public record CustomerDto(Guid Id, string FirstName, string LastName);
//{
//    public required Guid Id { get; init; }
//    public required string FirstName { get; init; }
//    public required string LastName { get; init; }
//}