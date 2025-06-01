using MediatR;
using Microsoft.Extensions.Logging;

namespace SovosTest.Application.Common.Behaviors;

/// <summary>
/// UnhandledExceptionBehavior handles any errors thrown by requests that are not handled 
/// by the request handlers themselves
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    /// <summary>B
    /// Creates a new instance of UnhandledExceptionBehavior
    /// </summary>
    /// <param name="logger"></param>
    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles any unhandled exception thrown by the request handlers
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}