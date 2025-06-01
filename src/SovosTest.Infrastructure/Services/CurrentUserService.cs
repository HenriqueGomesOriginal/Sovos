using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SovosTest.Application.Common.Exceptions;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Infrastructure.Services;

/// <summary>
/// Implementation for <see cref="ICurrentUserService"/>.
/// Uses ClaimsPrincipal to retrieve user information
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    /// <summary>
    /// Instantiate a new instance of <see cref="CurrentUserService"/>
    /// </summary>
    /// <param name="contextAccessor"></param>
    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    public string? Email => GetClaimsPrincipal().FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>
    /// Validate user context exists and returns <see cref="ClaimsPrincipal" />
    /// </summary>
    private ClaimsPrincipal GetClaimsPrincipal()
    {
        // Should be impossible to get here unauthenticated, but this is
        // an extra layer of defensive coding
        UnauthorizedUserException.ThrowIfNull(_contextAccessor);
        UnauthorizedUserException.ThrowIfNull(_contextAccessor.HttpContext);
        UnauthorizedUserException.ThrowIfNull(_contextAccessor.HttpContext.User);

        return _contextAccessor.HttpContext.User;
    }
}

