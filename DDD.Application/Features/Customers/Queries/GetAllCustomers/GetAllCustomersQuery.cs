using DDD.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Features.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllCustomersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Customers
            .Select(c => new CustomerDto(c.Id.Value, c.FirstName, c.LastName))
            .ToListAsync(cancellationToken);
    }
}