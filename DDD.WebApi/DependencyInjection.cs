using DDD.Application.Common.Interfaces;
using DDD.WebApi.Services;

namespace DDD.WebApi;

public static class DependencyInjection
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        //services.AddHealthChecks()
        //    .AddDbContextCheck<ApplicationDbContext>();

        //services.AddOpenApiDocument(configure => configure.Title = "CleanArchitecture API");

        services.AddEndpointsApiExplorer();
    }
}