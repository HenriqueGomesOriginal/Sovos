namespace SovosTest.Domain.Interfaces;

/// <summary>
/// Product repository, is responsible for manipulate Product table operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Returns all product's
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Entities.Product>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Return specific product by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Entities.Product> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Create a product
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Entities.Product> Create(Entities.Product product, CancellationToken cancellationToken);

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(Domain.Entities.Product product, CancellationToken cancellationToken);
}

