using Ardalis.Specification;

namespace DDD.Domain.Orders;

public class OrderByIdSpec : OrderSpec, ISingleResultSpecification<Order>
{
    public OrderByIdSpec(OrderId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
