using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Product.Commands.CreateProduct;

public record CreateProductCommand : IMapFrom<Domain.Entities.Product>, IRequest<CreateProductCommandResult>
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductCommand, Domain.Entities.Product>().
            ForMember(a => a.Id, a => a.MapFrom(b => Guid.NewGuid())).
            ForMember(a => a.Description, a => a.MapFrom(b => b.Description)).
            ForMember(a => a.Description, a => a.MapFrom(b => b.Price)).
            // ForMember(a => a.Description, a => a.MapFrom(b => b.Description)).
            ForMember(a => a.DomainEvents, a => a.Ignore());
    }
}
