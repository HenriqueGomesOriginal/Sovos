using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Infrastructure.Database;

/// <summary>
/// Database context used for Resuls management
/// </summary>
public class ContextDatabase : DbContext
{
    private const string RESULT_SCHEMA = "Result";
    private readonly ICurrentUserService _userService;

    /// <summary>
    /// Initializes a new instance of ContextDatabase
    /// </summary>
    /// <param name="options"></param>
    public ContextDatabase(DbContextOptions<ContextDatabase> options, ICurrentUserService userService)
        : base(options)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets or sets a list of Result
    /// </summary>
    /// <value></value>
    public virtual DbSet<Models.Product> Product { get; set; } = null!;

    /// <summary>
    /// Applies database configuration that lives in the assembly 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(RESULT_SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Override for SaveChangesAsync to include auditing.
    /// Base SaveChangesAsync() called
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}