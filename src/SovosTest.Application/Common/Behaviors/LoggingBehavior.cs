using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Management.Application.Common.Behaviors;

/// <summary>
/// Logging behavior intended to log incoming MediatR requests
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Creates a new instance of LoggingBehavior
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="currentUserService"></param>
    public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Logs incoming requests
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var email = _currentUserService.Email;

        _logger.LogInformation("Request: {Name} {@Email} {@Request}",
            requestName, email, request);

        return Task.CompletedTask;
    }
}