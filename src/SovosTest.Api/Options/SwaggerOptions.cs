using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SovosTest.Api.Options;

/// <summary>
/// Class designed to configure swagger and display available API versions and any XML productation.
/// </summary>
[ExcludeFromCodeCoverage]
public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
    /// </summary>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Configures XML comments, security requirements and versioned swagger docs.
    /// </summary>
    public void Configure(SwaggerGenOptions options)
    {
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        // Adds swagger JWT security.
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "bearer",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        // Loops through api version descriptions and adds them to the swagger doc.
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    /// <summary>
    /// Configures XML comments, security requirements and versioned swagger docs.
    /// </summary>
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "SovosTest API",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}