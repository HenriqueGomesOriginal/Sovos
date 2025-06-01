using SovosTest.Domain.Events;

namespace SovosTest.Application.Common.Interfaces;

/// <summary>
/// Interface to handle the publishing of domain events to the message bus
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Publish Domain Event
    /// </summary>
    /// <param name="messageSubject">The subject where the message should be published to</param>
    /// <param name="domainEvent"></param>
    /// <returns></returns>
    Task PublishAsync(string messageSubject, DomainEvent domainEvent);
}