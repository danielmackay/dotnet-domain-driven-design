namespace DDD.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty();
    }
}
