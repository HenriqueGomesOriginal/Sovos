using System.Diagnostics.CodeAnalysis;

namespace SovosTest.Domain.Common.Exceptions;


/// <summary>
/// Exception thrown when a Result is not found.
/// </summary>
public class ResultNotFoundException : Exception
{
    [ExcludeFromCodeCoverage]
    public ResultNotFoundException(string message) : base(message)
    {
    }
    
    [ExcludeFromCodeCoverage]
    public ResultNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}