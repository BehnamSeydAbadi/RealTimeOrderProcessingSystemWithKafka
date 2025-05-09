using Mediator;

namespace InventoryService.Application.Command.Inventory.Commands;

public class RegisterInventoryCommand : ICommand<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}