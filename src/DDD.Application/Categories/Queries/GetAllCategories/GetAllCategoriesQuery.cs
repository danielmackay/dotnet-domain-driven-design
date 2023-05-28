namespace DDD.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllCategoriesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .Select(c => new CategoryDto(c.Id.Value, c.Name))
            .ToListAsync(cancellationToken);
    }
}