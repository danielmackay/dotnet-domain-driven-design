using DDD.Application.LineItems.Queries.GetAllLineItems;
using DDD.Application.Orders.Commands.CreateLineItem;
using DDD.Application.Orders.Commands.CreateOrder;
using DDD.Application.Orders.Queries.GetAllLineItems;
using DDD.Application.Orders.Queries.GetAllOrders;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("orders")
            .WithTags("Orders")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetAllOrdersQuery(), ct))
            .WithName("GetOrders")
            .ProducesGet<OrderDto[]>();

        group
            .MapPost("/", (ISender sender, CreateOrderCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateOrder")
            .ProducesPost();

        group
            .MapPost("/{orderId:Guid}/lineitems", (ISender sender, Guid orderId, CreateLineItemCommand command, CancellationToken ct) =>
            {
                command.OrderId = orderId;
                sender.Send(command, ct);
            })
            .WithName("CreateOrderLineItem")
            .ProducesPost();

        group
            .MapGet("/{orderId:Guid}/lineitems", (ISender sender, Guid orderId, CancellationToken ct) => sender.Send(new GetAllLineItemsQuery(orderId), ct))
            .WithName("GetOrderLineItems")
            .ProducesGet<LineItemDto[]>();
    }
}