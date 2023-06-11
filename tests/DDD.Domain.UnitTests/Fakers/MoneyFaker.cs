using DDD.Domain.Common.Entities;

namespace DDD.Domain.UnitTests.Fakers;

public static class MoneyFaker
{
    public static Faker<Money> Create()
    {
        return new Faker<Money>().CustomInstantiator(f =>
            new Money(f.PickRandom(Currency.Currencies), f.Random.Decimal(1, 1000)));
    }
}