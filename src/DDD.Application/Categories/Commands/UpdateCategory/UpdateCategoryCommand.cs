using System.Text.Json.Serialization;

namespace DDD.Application.Categorys.Commands.UpdateCategory;

public record UpdateCategoryCommand(string Name) : IRequest
{
    [JsonIgnore]
    public Guid CategoryId { get; set; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICategoryService _categoryService;

    public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, ICategoryService categoryService)
    {
        _dbContext = dbContext;
        _categoryService = categoryService;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = new CategoryId(request.CategoryId);
        var spec = new CategoryByIdSpec(categoryId);
        var category = await _dbContext.Categories
            .WithSpecification(spec)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException();

        category.UpdateName(request.Name, _categoryService);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}