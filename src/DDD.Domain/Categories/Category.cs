namespace DDD.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public string Name { get; private set; } = default!;

    private Category() { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Category Create(string name)
    {
        Guard.Against.Empty(name);

        var category = new Category
        {
            Id = new CategoryId(Guid.NewGuid()),
            Name = name,
        };

        category.AddDomainEvent(new CategoryCreatedEvent(category.Id, category.Name));

        return category;
    }
}
