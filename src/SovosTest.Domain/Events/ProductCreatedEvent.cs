namespace SovosTest.Domain.Events;

public class ProductCreatedEvent : DomainEvent
{
    public ProductCreatedEvent(Entities.Product product)
    {
        Product = product;
    }

    public Entities.Product Product { get; set; }
}
