using Mediator;

namespace InventoryService.Application.Command.Inventory;

public class RegisterInventoryCommand : IRequest<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}