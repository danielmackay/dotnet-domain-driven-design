namespace DDD.Application.Orders.Commands.CreateCustomer;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty();
    }
}
