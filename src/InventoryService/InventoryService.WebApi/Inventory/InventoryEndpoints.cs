using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Query.Inventory;
using InventoryService.WebApi.Inventory.Dto;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.WebApi.Inventory;

public class InventoryEndpoints
{
    public static void Map(WebApplication webApplication)
    {
        webApplication.MapPost(
            "api/inventories",
            async ([FromBody] RegisterInventoryCommand command, IMediator mediator) =>
            {
                var inventoryId = await mediator.Send(command);
                return Results.Ok(inventoryId);
            }
        );

        webApplication.MapGet("api/inventories", async (IMediator mediator) =>
            {
                var inventories = await mediator.Send(new GetInventoriesQuery());
                return Results.Ok(inventories);
            }
        );

        webApplication.MapPost(
            "api/inventories/{id:guid}/products",
            async (Guid id, [FromBody] AddProductToInventoryDto dto, IMediator mediator) =>
            {
                var command = dto.Adapt<AddProductToInventoryCommand>();
                command.InventoryId = id;

                var productId = await mediator.Send(command);

                return Results.Ok(productId);
            }
        );

        webApplication.MapGet(
            "api/inventories/{id:guid}/products", async (Guid id, IMediator mediator) =>
            {
                var results = await mediator.Send(new GetInventoryProductsQuery(id));

                return Results.Ok(results);
            }
        );
    }
}