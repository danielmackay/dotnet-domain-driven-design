namespace DDD.Application.Categorys.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<Guid>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICategoryService _categoryService;

    public CreateCategoryCommandHandler(IApplicationDbContext dbContext, ICategoryService categoryService)
    {
        _dbContext = dbContext;
        _categoryService = categoryService;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, _categoryService);

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id.Value;
    }
}