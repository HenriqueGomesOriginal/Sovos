using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SovosTest.Infrastructure.Database.Configuration;

/// <summary>
/// Database mapping for Product
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Models.Product>
{
    /// <summary>
    /// Configures the DB model for Product
    /// </summary>
    /// <param DisplayFileName="builder"></param>
    public void Configure(EntityTypeBuilder<Models.Product> builder)
    {
        builder.ToTable("Product")
            .HasKey(p => p.Id);
        builder.Property(p => p.Id).HasMaxLength(255);
    }
}