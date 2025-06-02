using MediatR;
using SovosTest.Domain.Interfaces;
using AutoMapper;

namespace SovosTest.Application.Product.Queries.GetAll;

/// <summary>
/// Query to retrieve a Product by Id
/// </summary>
/// <value></value>
public sealed class GetAllQuery : IRequest<GetAllQueryResult>
{
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
        var requestedResult = await _productRepository.GetAll(cancellationToken);
        var mappedResult = _mapper.Map<List<GetAllQueryItem>>(requestedResult);

        return GetAllQueryResult.CreateSuccessResult(data: mappedResult);
    }
}