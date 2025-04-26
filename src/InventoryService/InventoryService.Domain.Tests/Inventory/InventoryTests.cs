using InventoryModel = InventoryService.Domain.Inventory.Inventory;
using FluentAssertions;
using InventoryService.Domain.Inventory.Dto;

namespace InventoryService.Domain.Tests.Inventory;

public class InventoryTests
{
    [Fact(DisplayName = "When an inventory is registered, it should be registered")]
    public void RegisteringInventory_ShouldBeRegistered()
    {
        var dto = new RegisterDto
        {
            Name = "Test",
            Location = "Test",
            Description = "Test"
        };

        var inventory = InventoryModel.Register(dto);

        inventory.Id.Should().NotBe(Guid.Empty);
        inventory.Name.Should().Be(dto.Name);
        inventory.Location.Should().Be(dto.Location);
        inventory.Description.Should().Be(dto.Description);
        inventory.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}