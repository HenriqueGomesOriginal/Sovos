using AutoMapper;
using MediatR;
using SovosTest.Domain.Interfaces;

namespace SovosTest.Application.Product.Commands.CreateProduct;

/// <summary>
/// Handler for CreateProductCommand
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Creates a new instance of CreateProductCommandHandler
    /// </summary>
    /// <param name="productRepository"></param>
    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // var existingProduct = await _productRepository.GetById(request, cancellationToken);

        // if (existingProduct is not null)
        //     return CreateProductCommandResult.DuplicateProductDetected(existingProduct.Id);

        var newProduct = _mapper.Map<Domain.Entities.Product>(request);

        var result = await _productRepository.Create(newProduct, cancellationToken);

        return CreateProductCommandResult.ProductCreated(result.Id);
    }
}