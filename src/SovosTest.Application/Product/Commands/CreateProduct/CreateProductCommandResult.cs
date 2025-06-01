namespace SovosTest.Application.Product.Commands.CreateProduct;

/// <summary>
/// Result of the CreateProductCommand handler
/// </summary>
/// <value></value>
public record CreateProductCommandResult
{
    /// <summary>
    /// Gets the newly generated ProductId
    /// </summary>
    /// <value></value>
    public Guid ProductId { get; }

    /// <summary>
    /// Gets the overall status of the result
    /// </summary>
    /// <value></value>
    public CreateProductCommandResultStatus Result { get; }

    /// <summary>
    /// Enum containing a list of potential return statuses 
    /// </summary>
    public enum CreateProductCommandResultStatus
    {
        ProductCreated = 1,
        DuplicateProductDetected = 2
    }

    /// <summary>
    /// Returns a new instance containing a ProductCreated result
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static CreateProductCommandResult ProductCreated(Guid productId)
    {
        return new(productId, CreateProductCommandResultStatus.ProductCreated);
    }

    /// <summary>
    /// Returns a new instance containing a DuplicateProductDetected result
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static CreateProductCommandResult DuplicateProductDetected(Guid productId)
    {
        return new(productId, CreateProductCommandResultStatus.DuplicateProductDetected);
    }

    /// <summary>
    /// Private constructor to enforce static method usage when generating results
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="result"></param>
    private CreateProductCommandResult(Guid productId, CreateProductCommandResultStatus result)
    {
        ProductId = productId;
        Result = result;
    }
}