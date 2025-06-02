using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SovosTest.Api.Models;
using SovosTest.Application.Product.Commands.CreateProduct;

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
}
