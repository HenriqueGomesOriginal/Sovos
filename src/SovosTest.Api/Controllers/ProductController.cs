using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SovosTest.Api.Models;
using SovosTest.Application.Product.Commands.CreateProduct;
using SovosTest.Application.Product.Queries.GetAll;

namespace SovosTest.Api.Controllers;

/// <summary>
/// Controller for creating / updating / retrieving Products
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Asp.Versioning.ApiVersion(AppApiVersion.V1)]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Creates a ProductController that uses MediatR
    /// </summary>
    /// <param name="mediator"></param>
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <sumary>
    /// Retrieves an OXP Product By Id 
    /// </sumary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    // [HttpGet("{ProductId:guid}")]
    // [Authorize(Policy = AppAuthorizationPolicy.ReadAccess)]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<GetProductGetAllResult>> GetAsync([FromRoute] GetProductGetAll query, CancellationToken cancellationToken)
    // {
    //     var product = await _mediator.Send(query, cancellationToken);

    //     var resultStatus = product?.Result;

    //     return resultStatus switch
    //     {
    //         GetProductGetAllResult.ProductGetAllResultStatus.ProductFound => Ok(product),
    //         GetProductGetAllResult.ProductGetAllResultStatus.ProductNotFound => NotFound(product),
    //         _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
    //     };
    // }

    /// <summary>
    /// Creates a new Product
    /// </summary>
    /// <remarks>
    /// Will return a 409 Conflict if a product already exists with the same name
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = AppAuthorizationPolicy.WriteAccess)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateProductCommandResult>> PostAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(request, cancellationToken);

        var resultStatus = product?.Result;

        return resultStatus switch
        {
            CreateProductCommandResult.CreateProductCommandResultStatus.ProductCreated => Created(HttpContext.Request.Path, product),
            CreateProductCommandResult.CreateProductCommandResultStatus.DuplicateProductDetected => Conflict(product),
            _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
        };
    }

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    /// <remarks>
    /// Will return a 404 Not Found if the product with the specified ID does not exist
    /// </remarks>
    /// <param name="id">The ID of the product to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    // [HttpDelete("{id}")]
    // [Authorize(Policy = AppAuthorizationPolicy.WriteAccess)]
    // [ProducesResponseType(StatusCodes.Status202Accepted)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    // {
    //     var request = new DeleteProductCommand { ProductId = id };
    //     var product = await _mediator.Send(request, cancellationToken);

    //     var resultStatus = product?.Result;

    //     return resultStatus switch
    //     {
    //         DeleteProductCommandResult.DeleteProductCommandResultStatus.ProductDeleted => Accepted(),
    //         DeleteProductCommandResult.DeleteProductCommandResultStatus.ProductNotFound => NotFound(),
    //         _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
    //     };
    // }
}
