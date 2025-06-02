using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SovosTest.Application.Common.Behaviors;
using SovosTest.Application.Common.Interfaces;
using SovosTest.Application.Common.Services;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SovosTest.Application;

/// <summary>
/// Static class containing dependencies for the application layer
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    /// <summary>
    /// Configure all application services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Adding automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // MediatR + Pipeline behaviors
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

        // Implementations
        services.AddTransient<IDomainEventService, DomainEventService>();

        return services;
    }
}