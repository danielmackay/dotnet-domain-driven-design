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

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(o => o.CategoryId)
            .IsRequired();
    }
}