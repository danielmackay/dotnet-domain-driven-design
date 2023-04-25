using DDD.Domain.Common.Entities;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
              .HasConversion(productId => productId.Value, value => new ProductId(value));

        builder.Property(p => p.Sku)
            .HasConversion(sku => sku.Value, value => Sku.Create(value)!)
            .HasMaxLength(50);

        builder.OwnsOne(p => p.Price, MoneyConfiguration.BuildAction);
    }
}

internal static class MoneyConfiguration
{
    internal static void BuildAction<T>(OwnedNavigationBuilder<T, Money> priceBuilder) where T : class
    {
        priceBuilder.Property(m => m.Currency).HasMaxLength(3);
        priceBuilder.Property(m => m.Amount).HasPrecision(18, 2);
    }
}