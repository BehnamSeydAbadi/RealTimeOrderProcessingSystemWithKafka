using InventoryService.Application.Command.Inventory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Application.Command;

public class ApplicationCommandsBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IOrderPlacementApplicationService, OrderPlacementApplicationService>();
    }
}