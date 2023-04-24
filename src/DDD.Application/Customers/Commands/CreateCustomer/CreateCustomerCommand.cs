using DDD.Domain.Customers;

namespace DDD.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Email, string FirstName, string LastName) : IRequest<Guid>;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateCustomerCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Email, request.FirstName, request.LastName);

        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}