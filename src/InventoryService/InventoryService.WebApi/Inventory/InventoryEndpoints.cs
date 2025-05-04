using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Query.Inventory;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.WebApi.Inventory;

public class InventoryEndpoints
{
    public static void Map(WebApplication webApplication)
    {
        webApplication.MapPost(
            "api/inventories", async ([FromBody] RegisterInventoryCommand command, IMediator mediator
            ) =>
            {
                var inventoryId = await mediator.Send(command);
                return Results.Ok(inventoryId);
            }
        );

        webApplication.MapGet(
            "api/inventories", async (IMediator mediator) =>
            {
                var inventories = await mediator.Send(new GetInventoriesQuery());
                return Results.Ok(inventories);
            }
        );
    }
}