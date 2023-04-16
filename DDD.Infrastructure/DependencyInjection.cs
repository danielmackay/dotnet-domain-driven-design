﻿using DDD.Application.Common.Interfaces;
using DDD.Infrastructure.Persistence;
using DDD.Infrastructure.Persistence.Interceptors;
using DDD.Infrastructure.Persistence.Repositories;
using DDD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<EntitySaveChangesInterceptor>();

        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddSingleton<IDateTime, DateTimeService>();

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}