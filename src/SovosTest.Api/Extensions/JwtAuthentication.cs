using Microsoft.AspNetCore.Authentication.JwtBearer;
using SovosTest.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace SovosTest.Api.Extensions;

/// <summary>
/// Configuration extensions to register services
/// required by authentication and authorization services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class JwtAuthentication
{
    private const string ApiScopeName = "sovos-api";

    /// <summary>
    /// Adds JWT authentication to the service collection with read, write polices.
    /// </summary>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add authentication and policies.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                 AddJwtBearer();

        services.AddCustomAuthorization();

        return services;
    }

    /// <summary>
    /// Adds JWT authentication to the service collection with read, write polices.
    /// </summary>
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppAuthorizationPolicy.WriteAccess,
                policy => policy.RequireClaim(AppScope.ScopeClaimType, $"{ApiScopeName}.{AppScope.Write}"));
            options.AddPolicy(AppAuthorizationPolicy.ReadAccess,
                policy => policy.RequireClaim(AppScope.ScopeClaimType, $"{ApiScopeName}.{AppScope.Read}"));

        });

        return services;
    }
}