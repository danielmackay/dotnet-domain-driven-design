using DDD.Domain.Orders;

namespace DDD.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrder(OrderId id);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}