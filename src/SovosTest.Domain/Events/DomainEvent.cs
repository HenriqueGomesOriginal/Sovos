using MediatR;

namespace SovosTest.Domain.Events;

/// <summary>
/// Abstract Domain Class that contains the definition of Events
/// </summary>
public abstract class DomainEvent : INotification
{
    /// <summary>
    /// Creates a new instance of DomainEvent
    /// </summary>
    protected DomainEvent()
    {
    }

    /// <summary>
    /// A bool indicating if the event has been published 
    /// </summary>
    /// <value></value>
    public bool IsPublished { get; set; }

    /// <summary>
    /// A DateTimeOffset containing the date/time of the event being published
    /// </summary>
    /// <value></value>
    public DateTimeOffset Created { get; protected set; } = DateTime.UtcNow;
}

