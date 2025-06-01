using SovosTest.Application.Common.Models;
using SovosTest.Application.Common.Interfaces;
using SovosTest.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SovosTest.Application.Product.Commands.CreateProduct.Events;

/// <summary>
/// Class designed to handle ProductCreated events 
/// </summary>
public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    private readonly IMessageService _messageService;
    private readonly IOptions<SubjectTemplatesConfig> _templateConfig;

    /// <summary>
    /// Creates a new instance of ProductCreatedEventHandler
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="messageService"></param>
    /// <param name="templateConfig">template for message subjects</param>
    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, IMessageService messageService, IOptions<SubjectTemplatesConfig> templateConfig)
    {
        _logger = logger;
        _messageService = messageService;
        _templateConfig = templateConfig;
    }

    /// <summary>
    /// Handles the ProductCreatedEvent by publishing it to the message bus
    /// </summary>
    /// <param name="createdEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(ProductCreatedEvent createdEvent, CancellationToken cancellationToken)
    {
        // TODO: Add Product Code here for logging
        _logger.LogInformation("Publishing message for Product");

        await _messageService.PublishAsync(_templateConfig.Value.ProductCreatedSubjectTemplate, createdEvent);
    }
}