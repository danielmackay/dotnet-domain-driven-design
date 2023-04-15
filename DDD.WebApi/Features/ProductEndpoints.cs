using DDD.Application.Features.Customers.Queries.GetAllCustomers;
using DDD.Application.Features.Products.Commands.CreateProduct;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Features;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("products")
            .WithTags("Products")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetAllProductsQuery(), ct))
            .WithName("GetProducts")
            .ProducesGet<ProductDto[]>();

        group
            .MapPost("/", (ISender sender, CreateProductCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateProduct")
            .ProducesPost();
    }
}