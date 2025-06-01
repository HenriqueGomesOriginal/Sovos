namespace SovosTest.Domain.Events;

/// <summary>
/// Interface describing IDomainEvents
/// </summary>
public interface IDomainEvents
{
    /// <summary>
    /// Contains a collection of DomainEvents
    /// </summary>
    /// <value></value>
    public ICollection<DomainEvent> DomainEvents { get; set; }
}