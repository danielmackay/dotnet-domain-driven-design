using DDD.Domain.Customers;
using DDD.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.Infrastructure.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
             .HasConversion(orderId => orderId.Value, value => new OrderId(value));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        // NOTE: If we had a Customer navigation property on Order, we would have to configure it like this:
        //builder.HasOne(o => o.Customer)
        //    .WithMany()
        //    .HasForeignKey(o => o.CustomerId);

        // NOTE: Line items can be exposed in two ways:
        // 1. Define a shadow field that tells EF to use the backing field to access the collection
        // 2. With a read only navigation property on the Order object
        builder.HasMany(o => o.LineItems)
            .WithOne()
            .HasForeignKey(li => li.OrderId)
            .IsRequired();
    }
}
