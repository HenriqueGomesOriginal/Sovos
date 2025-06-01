using SovosTest.Domain.Events;

namespace SovosTest.Application.Common.Interfaces;

/// <summary>
/// Interface to handle the publishing of domain events
/// </summary>
public interface IDomainEventService
{
    /// <summary>
    /// Publish Domain Events
    /// </summary>
    /// <param name="domainEvents"></param>
    /// <returns></returns>
    Task Publish(ICollection<DomainEvent> domainEvents);

    /// <summary>
    /// Publish Domain Event
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <returns></returns>
    Task Publish(DomainEvent domainEvent);
}