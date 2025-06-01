namespace SovosTest.Api.Models;

/// <summary>
/// Authorization policies supported by the API.
/// </summary>
public static class AppAuthorizationPolicy
{
    /// <summary>
    /// Controllers/Methods decorated with this policy will require WriteAccess
    /// in JWT scope.
    /// </summary>
    public const string WriteAccess = "WriteAccess";

    /// <summary>
    /// Controllers/Methods decorated with this policy will require ReadAccess
    /// in JWT scope.
    /// </summary>
    public const string ReadAccess = "ReadAccess";
}