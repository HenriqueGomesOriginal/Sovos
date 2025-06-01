using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Common.Behaviors;

/// <summary>
/// Behavior intended to track performance of requests coming into the pipeline
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Creates a new instance of the performance behavior class
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="currentUserService"></param>
    public PerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Handles incoming requests and logs long running operations
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Start the timer when the request enters the pipeline
        _timer.Start();

        // Waits for the response
        var response = await next();

        // Stop the timer after processing has finished
        _timer.Stop();

        // Grab the time in milliseconds 
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // If operation takes longer than 500 milliseconds, log it as a long running operation
        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var email = _currentUserService.Email;

            _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Email} {@Request}",
                requestName, elapsedMilliseconds, email, request);
        }

        return response;
    }
}