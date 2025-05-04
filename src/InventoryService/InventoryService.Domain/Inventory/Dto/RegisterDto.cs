namespace InventoryService.Domain.Inventory.Dto;

public record RegisterDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }
}