using DDD.Application.Common.Interfaces;
using DDD.Domain.DomainServices;
using DDD.Infrastructure.BackgroundJobs;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Interceptors;
using DDD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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

        AddQuartz(services);

        return services;
    }

    private static void AddQuartz(IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(10)
                                .RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();

        });

        services.AddQuartzHostedService();
    }
}