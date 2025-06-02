using System.Diagnostics.CodeAnalysis;

namespace SovosTest.Domain.Common.Exceptions;


/// <summary>
/// Exception thrown when a Result is not found.
/// </summary>
public class ProductNotFoundException : Exception
{
    [ExcludeFromCodeCoverage]
    public ProductNotFoundException(string message) : base(message)
    {
    }
    
    [ExcludeFromCodeCoverage]
    public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}