namespace DDD.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Sku)
            .NotEmpty();

        RuleFor(p => p.Amount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Currency)
            .NotEmpty();
    }
}
