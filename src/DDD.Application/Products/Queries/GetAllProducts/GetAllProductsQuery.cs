namespace DDD.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllProductsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Select(p => new ProductDto(p.Id.Value, p.Name, p.Sku.Value, p.Price.Amount, p.Price.Currency.Symbol, p.Category.Name))
            .ToListAsync(cancellationToken);
    }
}