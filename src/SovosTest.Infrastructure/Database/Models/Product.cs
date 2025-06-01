using SovosTest.Infrastructure.Models;

namespace SovosTest.Infrastructure.Database.Models;

public class Product : IBaseTable<Guid>
{
    /// <summary>
    /// Table identificator
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Category of the product
    /// </summary>
    public string Category { get; set; } = null!;
}