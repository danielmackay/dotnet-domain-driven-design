using Bogus;
using DDD.Domain.Common;
using DDD.Domain.Customers;
using DDD.Domain.Orders;
using DDD.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _dbContext;

    private const int NumProducts = 20;
    private const int NumCustomers = 20;
    private const int NumOrders = 20;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
                await _dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        await SeedProductsAsync();
        await SeedCustomersAsync();
        await SeedOrdersAsync();
    }

    private async Task SeedCustomersAsync()
    {
        if (await _dbContext.Customers.AnyAsync())
            return;

        var customerFaker = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(f.Person.FirstName, f.Person.LastName, f.Person.Email));

        var customers = customerFaker.Generate(NumCustomers);
        _dbContext.Customers.AddRange(customers);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedProductsAsync()
    {
        if (await _dbContext.Products.AnyAsync())
            return;

        var moneyFaker = new Faker<Money>()
            .CustomInstantiator(f => new Money(f.Finance.Currency().Code, f.Finance.Amount()));

        var skuFaker = new Faker<Sku>()
            .CustomInstantiator(f => Sku.Create(f.Commerce.Ean8())!);

        var faker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(f.Commerce.ProductName(), moneyFaker.Generate(), skuFaker.Generate()));

        var products = faker.Generate(NumProducts);
        _dbContext.Products.AddRange(products);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedOrdersAsync()
    {
        if (await _dbContext.Orders.AnyAsync())
            return;

        var customerIds = _dbContext.Customers.Select(c => c.Id).ToList();

        var orderFaker = new Faker<Order>()
            .CustomInstantiator(f => Order.Create(f.PickRandom(customerIds)));

        var orders = orderFaker.Generate(NumOrders);
        _dbContext.Orders.AddRange(orders);
        await _dbContext.SaveChangesAsync();
    }
}