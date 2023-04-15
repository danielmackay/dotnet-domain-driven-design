using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using DDD.Infrastructure.Common;
using DDD.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    private readonly IMediator _mediator;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options, EntitySaveChangesInterceptor saveChangesInterceptor, IMediator mediator) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_saveChangesInterceptor);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
