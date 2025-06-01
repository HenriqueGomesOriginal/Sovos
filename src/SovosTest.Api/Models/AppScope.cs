namespace SovosTest.Api.Models;

/// <summary>
/// Collection of constants for use in Scopes.
/// </summary>
public static class AppScope
{
    /// <summary>
    /// The name of the claim type to be used for Scopes.
    /// </summary>
    public const string ScopeClaimType = "scope";

    /// <summary>
    /// Scope for Read access.
    /// </summary>
    public const string Read = "read";

    /// <summary>
    /// Scope for Write access.
    /// </summary>
    public const string Write = "write";
}