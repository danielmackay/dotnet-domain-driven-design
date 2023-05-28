using Ardalis.Specification;

namespace DDD.Domain.Categories;

public class CategoryByIdSpec : Specification<Category>, ISingleResultSpecification<Category>
{
    public CategoryByIdSpec(CategoryId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
