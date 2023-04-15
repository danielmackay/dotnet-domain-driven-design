using DDD.Application.Common.Interfaces;
using DDD.Domain.Customers;
using DDD.Domain.Orders;

namespace DDD.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId) : IRequest<Guid>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = Order.Create(new CustomerId(request.CustomerId));

        _dbContext.Orders.Add(customer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}