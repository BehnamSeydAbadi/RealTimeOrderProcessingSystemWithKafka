using InventoryService.Application.Command.Common;
using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Exceptions;
using InventoryService.Domain.Inventory.Specifications;
using Mediator;

namespace InventoryService.Application.Command.Inventory.Commands;

public class AddProductToInventoryCommandHandler : AbstractCommandHandler<AddProductToInventoryCommand, Guid>
{
    private readonly IInventoryRepository _repository;

    public AddProductToInventoryCommandHandler(IPublisher publisher, IInventoryRepository repository) : base(publisher)
    {
        _repository = repository;
    }

    public override async ValueTask<Guid> Handle(
        AddProductToInventoryCommand command, CancellationToken cancellationToken
    )
    {
        var inventory = (await _repository.GetAsync<InventoryModel>(
            cancellationToken, new InventoryGetByIdSpecification(command.InventoryId)
        )).SingleOrDefault();
        Guard.Assert<InventoryNotFoundException>(inventory is null);

        var productId = inventory!.AddProduct(
            new ProductDto
            {
                Name = command.Name,
                StockKeepingUnit = command.StockKeepingUnit,
                Color = command.Color,
                UnitOfMeasure = command.UnitOfMeasure,
                IsPerishable = command.IsPerishable,
                Dimensions = command.Dimensions,
                Weight = command.Weight,
                Category = command.Category,
            }
        );

        await PublishDomainEventsAsync(inventory.GetDomainEventsQueue());

        return productId;
    }
}