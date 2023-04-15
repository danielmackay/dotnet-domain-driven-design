using Application.Features.TodoItems.Queries.GetAllTodoItems;
using DDD.Application.Features.TodoItems.Commands.CreateTodoItem;
using DDD.Application.Features.TodoItems.Queries.GetAllTodoItems;
using DDD.WebApi.Extensions;
using MediatR;

namespace DDD.WebApi.Features;

public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("todoitems")
            .WithTags("TodoItems")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct)
                => sender.Send(new GetAllTodoItemsQuery(), ct))
            .WithName("GetTodoItems")
            .ProducesGet<TodoItemDto[]>();

        // TODO: Investigate examples for swagger docs. i.e. better docs than:
        // myWeirdField: "string" vs myWeirdField: "this-silly-string"

        group
            .MapPost("/", (ISender sender, CreateTodoItemCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateTodoItem")
            .ProducesPost();
    }
}