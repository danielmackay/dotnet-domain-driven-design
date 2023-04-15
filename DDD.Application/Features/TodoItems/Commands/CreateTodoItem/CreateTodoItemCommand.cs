﻿using Application.Features.TodoItems.Specifications;
using Ardalis.Specification;
using Domain.Entities;
using Domain.Events;

namespace DDD.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string? Title) : IRequest<Guid>;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IRepositoryBase<TodoItem> _repository;

    public CreateTodoItemCommandValidator(
        IRepositoryBase<TodoItem> repository)
    {
        _repository = repository;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var spec = new TodoItemByTitleSpec(title);
        var exists = await _repository.AnyAsync(spec, cancellationToken);
        return !exists;
    }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IRepositoryBase<TodoItem> _repository;
    public CreateTodoItemCommandHandler(
        IMapper mapper,
        IPublisher publisher,
        IRepositoryBase<TodoItem> repository)
    {
        _mapper = mapper;
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<TodoItem>(request);

        todoItem.AddDomainEvent(new TodoItemCreatedEvent(todoItem));

        await _repository.AddAsync(todoItem, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return todoItem.Id.Value;
    }
}