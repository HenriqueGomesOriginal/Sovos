using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SovosTest.Api.Models;
using SovosTest.Application.Commands.CreateDocument;
using SovosTest.Application.Commands.DeleteDocument;
using SovosTest.Application.Queries.GetById;

namespace SovosTest.Api.Controllers;

/// <summary>
/// Controller for creating / updating / retrieving Documents
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(AppApiVersion.V1)]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Creates a DocumentController that uses MediatR
    /// </summary>
    /// <param name="mediator"></param>
    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <sumary>
    /// Retrieves an OXP Document By Id 
    /// </sumary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [HttpGet("{DocumentId:guid}")]
    [Authorize(Policy = AppAuthorizationPolicy.ReadAccess)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetDocumentByIdQueryResult>> GetAsync([FromRoute] GetDocumentByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(query, cancellationToken);

        var resultStatus = product?.Result;

        return resultStatus switch
        {
            GetDocumentByIdQueryResult.DocumentByIdQueryResultStatus.DocumentFound => Ok(product),
            GetDocumentByIdQueryResult.DocumentByIdQueryResultStatus.DocumentNotFound => NotFound(product),
            _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
        };
    }

    /// <summary>
    /// Creates a new Document
    /// </summary>
    /// <remarks>
    /// Will return a 409 Conflict if a document already exists with the same name
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = AppAuthorizationPolicy.WriteAccess)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateDocumentCommandResult>> PostAsync(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var product = await _mediator.Send(request, cancellationToken);

        var resultStatus = product?.Result;

        return resultStatus switch
        {
            CreateDocumentCommandResult.CreateDocumentCommandResultStatus.DocumentCreated => Created(HttpContext.Request.Path, product),
            CreateDocumentCommandResult.CreateDocumentCommandResultStatus.DuplicateDocumentDetected => Conflict(product),
            _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
        };
    }

    /// <summary>
    /// Deletes a document by its ID
    /// </summary>
    /// <remarks>
    /// Will return a 404 Not Found if the document with the specified ID does not exist
    /// </remarks>
    /// <param name="id">The ID of the document to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = AppAuthorizationPolicy.WriteAccess)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteDocumentCommand { DocumentId = id };
        var product = await _mediator.Send(request, cancellationToken);

        var resultStatus = product?.Result;

        return resultStatus switch
        {
            DeleteDocumentCommandResult.DeleteDocumentCommandResultStatus.DocumentDeleted => Accepted(),
            DeleteDocumentCommandResult.DeleteDocumentCommandResultStatus.DocumentNotFound => NotFound(),
            _ => throw new NotSupportedException($"The following product is not supported. Result: {resultStatus}")
        };
    }
}
