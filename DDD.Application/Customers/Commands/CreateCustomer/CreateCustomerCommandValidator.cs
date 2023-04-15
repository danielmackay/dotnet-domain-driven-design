using DDD.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateCustomerCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");

        RuleFor(p => p.FirstName)
            .NotEmpty();

        RuleFor(p => p.LastName)
            .NotEmpty();
    }

    private async Task<bool> BeUniqueTitle(string email, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Customers.AnyAsync(c => c.Email == email, cancellationToken);
        return !exists;
    }
}
