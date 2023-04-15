using DDD.Application.Common.Interfaces;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using DDD.Infrastructure.Common;
using DDD.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    private readonly IMediator _mediator;

    public required DbSet<Product> Products { get; set; }

    public required DbSet<Customer> Customers { get; set; }

    public required DbSet<Order> Orders { get; set; }

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
