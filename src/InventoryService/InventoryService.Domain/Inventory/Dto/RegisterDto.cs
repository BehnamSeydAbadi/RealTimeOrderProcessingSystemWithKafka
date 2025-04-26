namespace InventoryService.Domain.Inventory.Dto;

public record RegisterDto
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}