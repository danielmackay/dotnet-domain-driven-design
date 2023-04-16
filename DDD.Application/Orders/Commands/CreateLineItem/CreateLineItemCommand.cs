using DDD.Application.Common.Interfaces;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using System.Text.Json.Serialization;

namespace DDD.Application.Orders.Commands.CreateLineItem;

public record CreateLineItemCommand(Guid ProductId, int Quantity) : IRequest<Guid>
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
}

public class CreateLineItemCommandHandler : IRequestHandler<CreateLineItemCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateLineItemCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateLineItemCommand request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);
        var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
        ArgumentNullException.ThrowIfNull(product);

        var orderId = new OrderId(request.OrderId);
        var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        ArgumentNullException.ThrowIfNull(order);

        var lineItem = order.AddLineItem(productId, product.Price, request.Quantity);

        //_dbContext.LineItems.Add(customer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return lineItem.Id.Value;
    }
}