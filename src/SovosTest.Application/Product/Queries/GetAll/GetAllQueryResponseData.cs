using SovosTest.Application.Common.Interfaces;

namespace SovosTest.Application.Product.Queries.GetAll;

public record GetAllResponseData : IMapFrom<Domain.Entities.Product>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }

    public GetAllResponseData(int id, string name, string description, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }
}