using DDD.Application.Orders.Commands.AddPayment;
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
            .MapGet("/", async (ISender sender, CancellationToken ct) => await sender.Send(new GetAllOrdersQuery(), ct))
            .WithName("GetOrders")
            .ProducesGet<OrderDto[]>();

        group
            .MapPost("/", async (ISender sender, CreateOrderCommand command, CancellationToken ct) => await sender.Send(command, ct))
            .WithName("CreateOrder")
            .ProducesPost();

        group
            .MapPost("/{orderId:Guid}/lineitems", async (ISender sender, Guid orderId, CreateLineItemCommand command, CancellationToken ct) =>
            {
                command.OrderId = orderId;
                await sender.Send(command, ct);
            })
            .WithName("CreateOrderLineItem")
            .ProducesPost();

        group
            .MapGet("/{orderId:Guid}/lineitems", async (ISender sender, Guid orderId, CancellationToken ct) => await sender.Send(new GetAllLineItemsQuery(orderId), ct))
            .WithName("GetOrderLineItems")
            .ProducesGet<LineItemDto[]>();

        group
            .MapPost("/{orderId:Guid}/payment", async (ISender sender, Guid orderId, AddPaymentCommand command, CancellationToken ct) =>
            {
                command.OrderId = orderId;
                await sender.Send(command, ct);
            })
            .WithName("AddOrderPayment")
            .ProducesPost();
    }
}