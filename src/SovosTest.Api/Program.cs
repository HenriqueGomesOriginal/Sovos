using SovosTest.Api.Extensions;
using SovosTest.Api.Startup;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add logging to application
builder.Host.UseSerilog((builderContext, logger) => logger.ReadFrom.Configuration(builderContext.Configuration));

// add application services
builder.Services.ConfigureServices(builder.Configuration);

// add JWT auth separately so it can be configured ad-hoc for testing
builder.Services.AddJwtAuthentication(builder.Configuration);

// build the application
var app = builder.Build();

// add application features
app.ConfigureApplication();

// run the application
app.Run();
