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

        builder.HasMany(o => o.LineItems)
            .WithOne()
            .HasForeignKey(li => li.OrderId)
            .IsRequired();
    }
}
