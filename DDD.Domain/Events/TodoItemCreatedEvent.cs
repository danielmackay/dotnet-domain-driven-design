using DDD.Domain.Common;
using Domain.Entities;

namespace DDD.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item) => Item = item;

    public TodoItem Item { get; }
}