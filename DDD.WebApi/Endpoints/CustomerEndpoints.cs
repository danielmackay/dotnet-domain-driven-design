using DDD.Application.Customers.Commands.CreateCustomer;
using DDD.Application.Customers.Queries.GetAllCustomers;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("customers")
            .WithTags("Customers")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetAllCustomersQuery(), ct))
            .WithName("GetCustomers")
            .ProducesGet<CustomerDto[]>();

        group
            .MapPost("/", (ISender sender, CreateCustomerCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateTodoItem")
            .ProducesPost();
    }
}
