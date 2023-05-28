using DDD.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

internal static class MoneyConfiguration
{
    internal static void BuildAction<T>(OwnedNavigationBuilder<T, Money> priceBuilder) where T : class
    {
        priceBuilder.Property(m => m.Currency).HasMaxLength(3);
        priceBuilder.Property(m => m.Amount).HasPrecision(18, 2);
    }
}