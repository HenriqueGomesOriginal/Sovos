using AutoMapper;

namespace SovosTest.Infrastructure.Database.Mapping;

/// <summary>
/// Automapper mappings for Product and Domain entities
/// </summary>
public class ProductMapping : Profile
{
    /// <summary>
    /// Initialize a new ProductMapping
    /// </summary>
    public ProductMapping()
    {
        CreateProductProfile();
    }

    /// <summary>
    /// Mapping between db entity Product and the domain Product
    /// </summary>
    private void CreateProductProfile()
    {
        // Mapping from database model to domain
        CreateMap<Models.Product, Domain.Entities.Product>().
            ForMember(f => f.Name, f => f.MapFrom(m => m.Name)).
            ForMember(f => f.DomainEvents, f => f.Ignore());

        // Mapping from domain to database model 
        CreateMap<Domain.Entities.Product, Models.Product>().
            ForMember(f => f.Id, f => f.MapFrom(m => m.Id)).
            ForMember(f => f.Price, f => f.MapFrom(m => m.Price));
    }
}