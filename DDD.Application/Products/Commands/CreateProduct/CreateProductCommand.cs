using DDD.Application.Common.Interfaces;
using DDD.Domain.Common;
using DDD.Domain.Products;

namespace DDD.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Sku, decimal Amount, string Currency) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var price = new Money(request.Currency, request.Amount);

        var sku = Sku.Create(request.Sku);
        ArgumentNullException.ThrowIfNull(sku);

        var product = Product.Create(request.Name, price, sku);

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return product.Id.Value;
    }
}