using DDD.Domain.Common;

namespace DDD.Domain.UnitTests.Fakers;

public static class MoneyFaker
{
    public static Faker<Money> Create()
    {
        return new Faker<Money>().CustomInstantiator(f =>
            new Money(f.Finance.Currency().Code, f.Random.Decimal(1, 1000)));
    }
}