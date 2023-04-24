namespace DDD.Application.Orders.Commands.AddPayment;

public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
{
    public AddPaymentCommandValidator()
    {
        RuleFor(p => p.OrderId)
            .NotEmpty();

        RuleFor(p => p.Currency)
            .NotEmpty();

        RuleFor(p => p.Amount)
            .NotEmpty();
    }
}
