using DDD.Application.Categories.Queries.GetAllCategories;
using DDD.Application.Categorys.Commands.CreateCategory;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("categories")
            .WithTags("Categories")
            .WithOpenApi();

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) => await sender.Send(new GetAllCategoriesQuery(), ct))
            .WithName("GetCategories")
            .ProducesGet<CategoryDto[]>();

        group
            .MapPost("/", async (ISender sender, CreateCategoryCommand command, CancellationToken ct) => await sender.Send(command, ct))
            .WithName("CreateCategory")
            .ProducesPost();
    }
}