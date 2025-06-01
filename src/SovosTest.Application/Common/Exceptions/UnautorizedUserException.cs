using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SovosTest.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a a user/system attempts to access
/// a resource when unauthenticated 
/// </summary>
[Serializable]
public class UnauthorizedUserException : ArgumentNullException
{
    /// <summary>
    /// Instantiate a new instance of UnauthorizedUserException
    /// </summary>
    /// <param name="message"></param>
    public UnauthorizedUserException(string parameter)
        : base(parameter)
    {
    }

    /// <summary>
    /// Will validate a given argument is not null in a user
    /// authentication context.
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="argumentName"></param>
    /// <exception cref="UnauthorizedUserException"></exception>
    public new static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression("argument")] string? argumentName = null)
    {
        _ = argument ?? throw new UnauthorizedUserException(argumentName!);
    }

    /// <summary>
    /// Needed to fix a violation around serialization
    /// </summary>
#pragma warning disable SYSLIB0003 // Type or member is obsolete
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
#pragma warning restore SYSLIB0003 // Type or member is obsolete
    [ExcludeFromCodeCoverage]
    protected UnauthorizedUserException(SerializationInfo info, StreamingContext context) : base()
    {
    }

    /// <summary>
    /// Needed to fix a violation around serialization
    /// </summary>
#pragma warning disable SYSLIB0003 // Type or member is obsolete
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
#pragma warning restore SYSLIB0003 // Type or member is obsolete
    [ExcludeFromCodeCoverage]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}