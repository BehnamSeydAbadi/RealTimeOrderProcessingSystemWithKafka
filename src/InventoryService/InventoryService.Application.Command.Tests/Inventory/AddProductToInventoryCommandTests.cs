using FluentAssertions;
using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Command.Tests.Inventory.Stubs;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Exceptions;
using InventoryService.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Application.Command.Tests.Inventory;

public class AddProductToInventoryCommandTests
{
    [Fact(DisplayName = "When a product is added by in invalid inventory id, Then an exception should be thrown")]
    public async Task AddingProduct_WithInvalidInventoryId_ShouldThrowException()
    {
        var commandHandler = new AddProductToInventoryCommandHandler(
            StubPublisher.New(), StubInventoryRepository.New()
        );

        var action = async () => await commandHandler.Handle(
            new AddProductToInventoryCommand
            {
                InventoryId = Guid.NewGuid(),
                Name = "Apple",
                StockKeepingUnit = "APPLE-RED-1000-KG",
                Color = "Red",
                UnitOfMeasure = ProductUnitOfMeasure.Kg,
                IsPerishable = true,
                Dimensions = "10*10",
                Weight = "1000",
                Category = ProductCategory.Ingredients,
            },
            CancellationToken.None
        );

        await action.Should().ThrowExactlyAsync<InventoryNotFoundException>();
    }
}