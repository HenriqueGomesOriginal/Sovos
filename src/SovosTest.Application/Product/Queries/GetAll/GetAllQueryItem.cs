using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Product.Queries.GetAll;

public record GetAllQueryItem : IMapFrom<Domain.Entities.Product>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
}