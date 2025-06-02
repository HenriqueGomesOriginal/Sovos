using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SovosTest.Application.Common.Interfaces;
using SovosTest.Domain.Common.Exceptions;
using SovosTest.Domain.Entities;
using SovosTest.Domain.Events;
using SovosTest.Domain.Interfaces;
using SovosTest.Infrastructure.Database;

namespace SovosTest.Infrastructure.Repositories;

/// <summary>
/// Repository of operations for Product table
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ContextDatabase _context;
    private readonly IDomainEventService _domainEventService;
    private readonly IMapper _mapper;


    public ProductRepository(ContextDatabase context, IDomainEventService domainEventService, IMapper mapper)
    {
        _context = context;
        _domainEventService = domainEventService;
        _mapper = mapper;
    }

    /// <summary>
    /// Create Product entitie on database
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Domain.Entities.Product> Create(Domain.Entities.Product product, CancellationToken cancellationToken)
    {
        var dbResult = _mapper.Map<Models.Product>(product);

        _context.Add(dbResult);
        await _context.SaveChangesAsync();

        // Add new ProductCreatedEvent domain event and publish 
        product.DomainEvents.Add(new ProductCreatedEvent(product));
        await _domainEventService.Publish(product.DomainEvents);

        return product;
    }

    /// <summary>
    /// Delete some Product row based on a Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Delete(Domain.Entities.Product product, CancellationToken cancellationToken)
    {
        var existingEntity = await _context.Product.FindAsync(product.Id, cancellationToken);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
            _context.Product.Update(existingEntity);
            _context.Product.Remove(existingEntity);

            await _context.SaveChangesAsync(cancellationToken);

            // Add new ProductDeletedEvent domain event and publish
            product.DomainEvents.Add(new ProductDeletedEvent(product));
            await _domainEventService.Publish(product.DomainEvents);
        }
        else
        {
            throw new ResultNotFoundException($"Product not found. ResultId: {product.Id}");
        }
    }

    /// <summary>
    /// List all Product's entities on database (This needs to be paginated on the future)
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<Domain.Entities.Product>> GetAll(CancellationToken cancellationToken)
    {
        var product = await _context.Product.ToListAsync(cancellationToken);

        return _mapper.Map<List<Domain.Entities.Product>>(product);
    }

    /// <summary>
    /// Get a single Product based on specific Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Product> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _context.Product.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (product != null)
        {
            return _mapper.Map<Domain.Entities.Product>(product);
        }
        else
        {
            throw new ResultNotFoundException($"Product not found. ProductId: {id}");
        }
    }
}