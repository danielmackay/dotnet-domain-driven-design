using DDD.Application.Common.Interfaces;
using DDD.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext _dbContext;

    public OrderRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order?> GetOrder(OrderId id) =>
        await _dbContext.Orders.Include(i => i.LineItems).FirstOrDefaultAsync(o => o.Id == id);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
