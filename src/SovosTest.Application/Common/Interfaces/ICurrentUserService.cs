namespace SovosTest.Application.Common.Interfaces;

/// <summary>
/// Service intended to retrieve metadata from the current user's session
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Retrieves the users email from the current session
    /// </summary>
    /// <returns></returns>
    string? Email { get; }
}