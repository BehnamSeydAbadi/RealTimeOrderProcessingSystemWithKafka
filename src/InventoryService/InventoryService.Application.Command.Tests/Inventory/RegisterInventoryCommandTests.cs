using FluentAssertions;
using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Command.Tests.Inventory.Stubs;
using InventoryService.Domain.Inventory.Exceptions;

namespace InventoryService.Application.Command.Tests.Inventory;

public class RegisterInventoryCommandTests
{
    [Fact(DisplayName =
        "When an inventory gets registered with duplicate code, Then an exception should be thrown")]
    public async Task RegisterInventory_WithDuplicateCode_ShouldThrowException()
    {
        var commandHandler = new RegisterInventoryCommandHandler(
            StubPublisher.New(), StubDuplicateInventoryCodeDomainService.New().WithIsInventoryCodeDuplicateValue(true)
        );

        var action = async () => await commandHandler.Handle(
            new RegisterInventoryCommand
            {
                Code = "Test",
                Name = "Test",
                Location = "Test",
                Description = "Test",
            },
            CancellationToken.None
        );

        await action.Should().ThrowAsync<DuplicateInventoryCodeException>();
    }
}