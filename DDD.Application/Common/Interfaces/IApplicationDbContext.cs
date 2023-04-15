using DDD.Domain.Customers;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace DDD.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}