using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SovosTest.Application.Common.Interfaces;
using SovosTest.Infrastructure.Services;
using System.Reflection;
using SovosTest.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using SovosTest.Domain.Interfaces;
using SovosTest.Infrastructure.Repositories;
using AutoMapper;

namespace SovosTest.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    // Application settings key for the postgresql connection
    private const string CONNECTION_STRING_KEY = "ClientContext";
    private const string POSTGRESQL_SUPPORTED_VERSION_KEY = "PostgreSQL:SupportedVersion";

    /// <summary>
    /// Configure all application services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<ContextDatabase>(o => o.UseInMemoryDatabase("ContextDatabase"));

        // Needed to inject ClaimsPrinciple used by CurrentUserService
        services.AddHttpContextAccessor();

        // Implementations
        services.AddTransient<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddHealthChecks().
            AddDbContextCheck<ContextDatabase>();

        return services;
    }
}