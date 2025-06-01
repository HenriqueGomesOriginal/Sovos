using SovosTest.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Common.Services;

/// <summary>
/// Implementation for IDomainEventService
/// </summary>
public class DomainEventService : IDomainEventService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IPublisher _mediator;

    /// <summary>
    /// Creates a new instance of DomainEventService
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mediator"></param>
    public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <inheritdoc/>
    public async Task Publish(ICollection<DomainEvent> domainEvents)
    {
        // Get list of events that need to be published
        var publishableEvents = domainEvents.Where(i => !i.IsPublished).
                                             ToList();

        _logger.LogInformation("{count} events will be published", publishableEvents.Count);

        foreach (var domainEvent in publishableEvents)
        {
            await Publish(domainEvent);
        }
    }

    /// <inheritdoc/>
    public async Task Publish(DomainEvent domainEvent)
    {
        _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);

        await _mediator.Publish(domainEvent);

        domainEvent.IsPublished = true;

        _logger.LogInformation("{event} published", domainEvent.GetType().Name);
    }
}