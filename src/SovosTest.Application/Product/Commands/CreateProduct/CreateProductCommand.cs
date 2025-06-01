using System.ComponentModel.DataAnnotations;
using MediatR;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Product.Commands.CreateProduct;

public record CreateProductCommand : IValidatableObject, IMapFrom<Domain.Entities.Product>, IRequest<CreateProductCommandResult>
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    [Required]
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    [Required]
    public required string Description { get; init; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    [Required]
    public required decimal Price { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Price <= 0)
        {
            yield return new ValidationResult("Price must be greater than zero.", new[] { nameof(Price) });
        }
    }
}
