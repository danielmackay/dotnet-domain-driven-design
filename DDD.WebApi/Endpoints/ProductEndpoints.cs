using DDD.Application.Products.Commands.CreateProduct;
using DDD.Application.Products.Queries.GetAllProducts;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("products")
            .WithTags("Products")
            .WithOpenApi();

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) => await sender.Send(new GetAllProductsQuery(), ct))
            .WithName("GetProducts")
            .ProducesGet<ProductDto[]>();

        group
            .MapPost("/", async (ISender sender, CreateProductCommand command, CancellationToken ct) => await sender.Send(command, ct))
            .WithName("CreateProduct")
            .ProducesPost();
    }
}