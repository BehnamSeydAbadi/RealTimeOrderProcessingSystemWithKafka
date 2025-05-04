using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Query.Inventory;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Events;
using InventoryService.Infrastructure.Inventory;
using Mapster;

namespace InventoryService.WebApi.Inventory;

public class InventoryMapper
{
    public static void Register()
    {
        TypeAdapterConfig<InventoryEntity, InventoryModel>.NewConfig();
        TypeAdapterConfig<RegisterInventoryCommand, RegisterDto>.NewConfig();
        TypeAdapterConfig<InventoryRegisteredEvent, InventoryEntity>.NewConfig();
        TypeAdapterConfig<InventoryEntity, InventoryViewModel>.NewConfig();
    }
}