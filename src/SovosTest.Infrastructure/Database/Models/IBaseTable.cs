namespace SovosTest.Infrastructure.Models;

/// <summary>
/// Generic interface for standard table fields
/// </summary>
public interface IBaseTable<T>
{
    /// <summary>
    /// Gets or sets the primary key
    /// </summary>
    /// <value></value>
    T Id { get; set; }
}