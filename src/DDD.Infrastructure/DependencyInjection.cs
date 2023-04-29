using DDD.Application.Common.Interfaces;
using DDD.Domain.DomainServices;
using DDD.Infrastructure.BackgroundJobs;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Interceptors;
using DDD.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
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

        AddHangfire(services, config);

        return services;
    }

    private static void AddHangfire(IServiceCollection services, IConfiguration config)
    {
        // Add Hangfire services.
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(config.GetConnectionString("DefaultConnection")));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
    }

    public static void UseInfrastructure(this IApplicationBuilder builder)
    {
        // This can be viewed at {localhost}/hangfire
        builder.UseHangfireDashboard();

        var manager = builder.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        manager.AddOrUpdate<ProcessOutboxMessagesJob>("outbox-messages", x => x.Execute(CancellationToken.None), Cron.Minutely);
    }
}