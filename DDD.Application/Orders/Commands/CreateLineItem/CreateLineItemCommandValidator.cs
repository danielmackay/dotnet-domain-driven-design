namespace DDD.Application.Orders.Commands.CreateLineItem;

public class CreateLineItemCommandValidator : AbstractValidator<CreateLineItemCommand>
{
    public CreateLineItemCommandValidator()
    {
        RuleFor(p => p.OrderId)
            .NotEmpty();

        RuleFor(p => p.ProductId)
            .NotEmpty();

        RuleFor(p => p.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}
