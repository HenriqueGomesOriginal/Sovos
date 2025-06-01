using MediatR;
using System.ComponentModel.DataAnnotations;
using SovosTest.Domain.Interfaces;
using AutoMapper;

namespace SovosTest.Application.Product.Queries.GetAll;

/// <summary>
/// Query to retrieve a Product by Id
/// </summary>
/// <value></value>
public sealed class GetAllQuery : IRequest<GetAllQueryResult>
{
    /// <summary>
    /// Gets product id 
    /// </summary>
    /// <value></value>
    [Required]
    public required Guid ProductId { get; init; }
}

/// <summary>
/// Query handler to retrieve a product by their Id
/// </summary>
/// <value></value>
public class GetProductGetAllHandler : IRequestHandler<GetAllQuery, GetAllQueryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductGetAllHandler(IProductRepository productsRepository, IMapper mapper)
    {
        _productRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<GetAllQueryResult> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var requestProduct = await _productRepository.GetAll(cancellationToken);

        //If we don't find the requested product, return not found
        if (requestProduct is null)
            return GetAllQueryResult.NotFound();

        var responseItemResult = _mapper.Map<GetAllQueryResponseData>(requestProduct);

        return GetAllQueryResult.Found(responseItemResult);
    }
}