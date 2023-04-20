using DDD.Application.Common.Interfaces;
using DDD.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Orders.Queries.GetAllLineItems;

public record GetAllLineItemsQuery(Guid OrderId) : IRequest<IEnumerable<LineItemDto>>;

public class GetAllLineItemsQueryHandler : IRequestHandler<GetAllLineItemsQuery, IEnumerable<LineItemDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllLineItemsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<LineItemDto>> Handle(GetAllLineItemsQuery request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        var spec = new OrderByIdSpec(orderId);
        return await _dbContext.Orders
            .WithSpecification(spec)
            .SelectMany(o => o.LineItems)
            .Select(li => new LineItemDto(
                li.Id.Value,
                new ProductDto(li.ProductId.Value, li.Product!.Name, li.Product.Sku.Value),
                new MoneyDto(li.Price.Currency, li.Price.Amount)))
            .ToListAsync(cancellationToken);
    }
}