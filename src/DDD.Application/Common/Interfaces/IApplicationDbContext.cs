using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;

namespace DDD.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}