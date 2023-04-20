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
        var productSpec = new ProductByIdSpec(productId);
        var product = await _dbContext.Products
            .WithSpecification(productSpec)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException();

        var orderId = new OrderId(request.OrderId);
        var orderSpec = new OrderByIdSpec(orderId);
        var order = await _dbContext.Orders
            .WithSpecification(orderSpec)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException();

        var lineItem = order.AddLineItem(productId, product.Price, request.Quantity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return lineItem.Id.Value;
    }
}