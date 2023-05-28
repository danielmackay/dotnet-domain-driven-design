using DDD.Application.Categories.Queries.GetAllCategories;
using DDD.Application.Categorys.Commands.CreateCategory;
using DDD.Application.Categorys.Commands.UpdateCategory;
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

        group
            .MapPut("/{categoryId:Guid}", async (ISender sender, Guid categoryId, UpdateCategoryCommand command, CancellationToken ct) =>
            {
                command.CategoryId = categoryId;
                await sender.Send(command, ct);
                return Results.NoContent();
            })
            .WithName("UpdateCategory")
            .ProducesPut();
    }
}