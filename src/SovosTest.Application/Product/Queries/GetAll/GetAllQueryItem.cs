using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Product.Queries.GetAll;

public record GetAllQueryItem : IMapFrom<Domain.Entities.Product>
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public decimal Price { get; init; }
}