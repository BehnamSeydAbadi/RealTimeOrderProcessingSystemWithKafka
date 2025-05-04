using FluentAssertions;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Product;

namespace InventoryService.Domain.Tests.Inventory;

public class InventoryModelTests
{
    [Fact(DisplayName = "When an inventory is registered, it should be registered")]
    public void RegisteringInventory_ShouldBeRegistered()
    {
        var dto = new RegisterDto
        {
            Code = "Test",
            Name = "Test",
            Location = "Test",
            Description = "Test"
        };

        var inventory = InventoryModel.Register(dto);

        inventory.Id.Should().NotBe(Guid.Empty);
        inventory.Code.Should().Be(dto.Code);
        inventory.Name.Should().Be(dto.Name);
        inventory.Location.Should().Be(dto.Location);
        inventory.Description.Should().Be(dto.Description);
        inventory.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName =
        "There is a registered inventory, When a product is added to the inventory, it should be added to the inventory")]
    public void AddingProduct_ShouldBeAddedToTheInventory()
    {
        var inventory = InventoryModel.Register(new RegisterDto
        {
            Code = "0101",
            Name = "Tehran inventory",
            Location = "Tehran",
            Description = "Just an inventory"
        });

        var productDto = new ProductDto
        {
            Name = "Apple",
            StockKeepingUnit = "APPLE-RED-1000-KG",
            Color = "Red",
            UnitOfMeasure = ProductUnitOfMeasure.Kg,
            IsPerishable = true,
            Dimensions = "10*10",
            Weight = "1000",
            Category = ProductCategory.Ingredients,
        };

        inventory.AddProduct(productDto);

        inventory.Products.Should().NotBeEmpty();
        inventory.Products[0].Name.Should().Be(productDto.Name);
        inventory.Products[0].StockKeepingUnit.Should().Be(productDto.StockKeepingUnit);
        inventory.Products[0].Color.Should().Be(productDto.Color);
        inventory.Products[0].UnitOfMeasure.Should().Be(productDto.UnitOfMeasure);
        inventory.Products[0].IsPerishable.Should().Be(productDto.IsPerishable);
        inventory.Products[0].Dimensions.Should().Be(productDto.Dimensions);
        inventory.Products[0].Weight.Should().Be(productDto.Weight);
        inventory.Products[0].Category.Should().Be(productDto.Category);
    }
}