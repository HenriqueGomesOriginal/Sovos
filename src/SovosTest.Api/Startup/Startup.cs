using System.Diagnostics.CodeAnalysis;
using SovosTest.Api.Options;
using SovosTest.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HeaderPropagation;

namespace SovosTest.Api.Startup;

/// <summary>
/// The startup class handles configuration of services and applications needed to run the webapp
/// </summary>
[ExcludeFromCodeCoverage]
public static class Startup
{
    /// <summary>
    /// Configures the application services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // add infrastructure layer
        services.AddInfrastructureServices(configuration);

        // add application layer
        //services.AddApplicationServices();

        // Add the HeaderPropagation middleware with the "Authorization" and "CorrelationId" headers to the DI container.
        services.AddHeaderPropagation(options =>
        {
            options.Headers.Add("Authorization");
            options.Headers.Add("CorrelationId");
        });

        // add Api versioning 
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Need to configure ForwardedHeaders for k8s support as the reverse proxy terminates TLS internally before it reaches this API 
        // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-3.1#other-proxy-server-and-load-balancer-scenarios
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddControllers();

        services.AddSwaggerGen();

        services.AddControllers().AddJsonOptions(option =>
        {
            // Create a new instance of JsonStringEnumConverter and add it to the Converters collection of the JsonSerializerOptions instance.
            // This will ensure that enums are serialized as their string values instead of their integer values.
            option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    /// <summary>
    /// Configures the application startup
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Configures swagger to display each versioned API page.
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions.Select(s => s.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{description}/swagger.json",
                        description.ToUpperInvariant());
                }
            });
        }
        else
        {
            // this should forward the headers from the proxy/load balancer in k8s
            app.UseForwardedHeaders();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapHealthChecks("/health");
        app.MapControllers().
            RequireAuthorization();
    }
}