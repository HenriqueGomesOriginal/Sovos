namespace SovosTest.Domain.Events;

public class ProductDeletedEvent : DomainEvent
{
    public ProductDeletedEvent(Entities.Product product) 
    {
        Product = product;
    }

    public Product Product { get; set; }
}
