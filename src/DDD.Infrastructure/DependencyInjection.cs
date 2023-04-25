using DDD.Application.Common.Interfaces;
using DDD.Domain.DomainServices;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Interceptors;
using DDD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        services.AddSingleton<IDateTime, DateTimeService>();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();
        services.AddScoped<OutboxInterceptor>();


        return services;
    }
}