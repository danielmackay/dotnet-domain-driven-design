using Ardalis.Specification;

namespace DDD.Domain.Orders;

public class OrderSpec : Specification<Order>
{
    public OrderSpec()
    {
        Query.Include(i => i.LineItems);
    }
}
