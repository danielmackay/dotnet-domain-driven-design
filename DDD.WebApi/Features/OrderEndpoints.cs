﻿using DDD.Application.Features.Orders.Queries.GetAllOrders;
using DDD.Application.Orders.Queries.GetAllOrders;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Features;

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

        //group
        //    .MapPost("/", (ISender sender, CreateProductCommand command, CancellationToken ct) => sender.Send(command, ct))
        //    .WithName("CreateProduct")
        //    .ProducesPost();
    }
}