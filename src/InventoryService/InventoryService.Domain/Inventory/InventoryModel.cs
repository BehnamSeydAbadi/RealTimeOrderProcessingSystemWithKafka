using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Events;
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
    public List<ProductModel> Products { get; private set; } = new();


    public Guid AddProduct(ProductDto productDto)
    {
        var productAddedEvent = new ProductAddedToInventoryEvent(
            this.Id, Guid.NewGuid(), productDto.Name, productDto.StockKeepingUnit, productDto.Color,
            productDto.UnitOfMeasure, productDto.IsPerishable, productDto.Dimensions, productDto.Weight,
            productDto.Category
        );

        EnqueueDomainEvent(productAddedEvent);
        Mutate(productAddedEvent);

        return productAddedEvent.ProductId;
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
}