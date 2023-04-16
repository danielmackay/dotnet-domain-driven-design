using DDD.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllOrdersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Select(o => new OrderDto(o.Id.Value, new CustomerDto(o.CustomerId.Value, o.Customer!.FirstName, o.Customer.LastName), o.OrderTotal))
            .ToListAsync(cancellationToken);
    }
}