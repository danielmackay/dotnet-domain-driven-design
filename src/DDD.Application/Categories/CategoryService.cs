using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Application.Categories;

public class CategoryService : ICategoryService
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CategoryService(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


    public bool CategoryExists(string categoryName)
    {
        return _applicationDbContext.Categories.Any(c => c.Name == categoryName);
    }
}
