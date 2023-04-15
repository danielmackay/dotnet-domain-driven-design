using DDD.Domain.Orders;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

internal class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        // Needed as Plurialisation only happens for entities that have a DbSet on the DbContext
        builder.ToTable("LineItems");

        builder.HasKey(li => li.Id);

        builder.Property(li => li.Id)
            .HasConversion(lineItemId => lineItemId.Value, value => new LineItemId(value));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(li => li.ProductId)
            .IsRequired();

        // Do we need to configure the max length of currency here also?
        builder.OwnsOne(li => li.Price, MoneyConfiguration.BuildAction);


    }
}
