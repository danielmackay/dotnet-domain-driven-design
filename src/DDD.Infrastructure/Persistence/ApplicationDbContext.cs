using DDD.Application.Common.Interfaces;
using DDD.Domain.Categories;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using DDD.Infrastructure.Outbox;
using DDD.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    private readonly OutboxInterceptor _outboxInterceptor;

    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<Customer> Customers { get; set; } = default!;

    public DbSet<Order> Orders { get; set; } = default!;

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions options, EntitySaveChangesInterceptor saveChangesInterceptor, OutboxInterceptor outboxInterceptor) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
        _outboxInterceptor = outboxInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(
            _saveChangesInterceptor,
            _outboxInterceptor);
    }
}
