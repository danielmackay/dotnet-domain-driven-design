using DDD.Domain.Products;

namespace DDD.Domain.UnitTests.Fakers;

public static class SkuFaker
{
    public static Faker<Sku> Create()
    {
        return new Faker<Sku>().CustomInstantiator(f => Sku.Create(f.Commerce.Ean8())!);
    }
}
