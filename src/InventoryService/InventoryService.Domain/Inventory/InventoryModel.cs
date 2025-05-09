using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Events;
using InventoryService.Domain.Inventory.Exceptions;
using InventoryService.Domain.Product;
using Mapster;

namespace InventoryService.Domain.Inventory;

public class InventoryModel : AbstractAggregateRoot
{
    public static InventoryModel Register(RegisterDto dto)
    {
        var inventoryRegisteredEvent = new InventoryRegisteredEvent(
            Guid.NewGuid(), dto.Code, dto.Name, dto.Location, dto.Description, DateTime.UtcNow, dto.IsActive
        );

        var inventory = new InventoryModel();

        inventory.EnqueueDomainEvent(inventoryRegisteredEvent);
        inventory.Mutate(inventoryRegisteredEvent);

        return inventory;
    }


    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public bool IsActive { get; private set; }
    public List<ProductModel> Products { get; private set; } = [];


    public Guid AddProduct(ProductDto productDto)
    {
        Guard.Assert<InActiveInventoryException>(this.IsActive is false);

        var productAddedEvent = new ProductAddedToInventoryEvent(
            this.Id, Guid.NewGuid(), productDto.Name, productDto.StockKeepingUnit, productDto.Color,
            productDto.UnitOfMeasure, productDto.IsPerishable, productDto.Dimensions, productDto.Weight,
            productDto.Category
        );

        EnqueueDomainEvent(productAddedEvent);
        Mutate(productAddedEvent);

        return productAddedEvent.ProductId;
    }

    public bool AnyProduct(Guid productId) => this.Products.Any(p => p.Id == productId);

    public void RemoveProduct(Guid productId)
    {
        Guard.Assert<InActiveInventoryException>(this.IsActive is false);
        Guard.Assert<ProductNotFoundException>(this.Products.Any(p => p.Id == productId) is false);

        var productRemovedFromInventoryEvent = new ProductRemovedFromInventoryEvent(this.Id, productId);

        EnqueueDomainEvent(productRemovedFromInventoryEvent);
        Mutate(productRemovedFromInventoryEvent);
    }

    public void BecomeInactive()
    {
        var inventoryBecameInactiveEvent = new InventoryBecameInactiveEvent(this.Id);

        EnqueueDomainEvent(inventoryBecameInactiveEvent);
        Mutate(inventoryBecameInactiveEvent);
    }

    public void Activate()
    {
        var inventoryBecameInactiveEvent = new InventoryActivatedEvent(this.Id);

        EnqueueDomainEvent(inventoryBecameInactiveEvent);
        Mutate(inventoryBecameInactiveEvent);
    }


    protected override void When(AbstractDomainEvent domainEvent) => On((dynamic)domainEvent);


    private void On(InventoryRegisteredEvent domainEvent)
    {
        this.Id = domainEvent.Id;
        this.Code = domainEvent.Code;
        this.Name = domainEvent.Name;
        this.Location = domainEvent.Location;
        this.Description = domainEvent.Description;
        this.IsActive = domainEvent.IsActive;
        this.RegisteredAt = domainEvent.RegisteredAt;
    }

    private void On(ProductAddedToInventoryEvent domainEvent)
    {
        this.Products.Add(
            new ProductModel(
                domainEvent.ProductId,
                domainEvent.ProductName,
                domainEvent.ProductStockKeepingUnit,
                domainEvent.ProductColor,
                domainEvent.ProductUnitOfMeasure,
                domainEvent.ProductIsPerishable,
                domainEvent.ProductDimensions,
                domainEvent.ProductWeight,
                domainEvent.ProductCategory
            )
        );
    }

    private void On(ProductRemovedFromInventoryEvent domainEvent)
    {
        this.Products.RemoveAll(p => p.Id == domainEvent.ProductId);
    }

    private void On(InventoryBecameInactiveEvent domainEvent)
    {
        this.IsActive = false;
    }

    private void On(InventoryActivatedEvent domainEvent)
    {
        this.IsActive = true;
    }
}