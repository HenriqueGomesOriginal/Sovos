namespace SovosTest.Domain.Entities;

/// <summary>
/// A Domain class for Product table
/// </summary>  
public class Product
{
    /// <summary>
    /// Table identificator
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the category of the product
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the product
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of domain events associated with the product.
    /// </summary>
    public ICollection<DomainEvent> DomainEvents { get; set; } = new HashSet<DomainEvent>();

}
