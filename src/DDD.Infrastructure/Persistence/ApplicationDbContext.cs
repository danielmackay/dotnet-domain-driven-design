using DDD.Application.Common.Interfaces;
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
    private readonly DispatchDomainEventsInterceptor _dispatchDomainEventsInterceptor;
    private readonly OutboxInterceptor _outboxInterceptor;

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public ApplicationDbContext(DbContextOptions options, EntitySaveChangesInterceptor saveChangesInterceptor, DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor, OutboxInterceptor outboxInterceptor) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
        _dispatchDomainEventsInterceptor = dispatchDomainEventsInterceptor;
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
            //_dispatchDomainEventsInterceptor,
            _outboxInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
