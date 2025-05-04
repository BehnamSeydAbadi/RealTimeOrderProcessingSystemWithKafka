using InventoryService.Application.Command.Common;
using InventoryService.Domain.Common;
using InventoryService.Domain.DomainService;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Exceptions;
using Mediator;

namespace InventoryService.Application.Command.Inventory;

public class RegisterInventoryCommandHandler : AbstractCommandHandler<RegisterInventoryCommand, Guid>
{
    private readonly IDuplicateInventoryCodeDomainService _duplicateInventoryCodeDomainService;

    public RegisterInventoryCommandHandler(
        IPublisher publisher, IDuplicateInventoryCodeDomainService duplicateInventoryCodeDomainService
    ) : base(publisher)
    {
        _duplicateInventoryCodeDomainService = duplicateInventoryCodeDomainService;
    }

    public override async ValueTask<Guid> Handle(RegisterInventoryCommand request, CancellationToken cancellationToken)
    {
        Guard.Assert<DuplicateInventoryCodeException>(
            await _duplicateInventoryCodeDomainService.IsInventoryCodeDuplicateAsync(request.Code, cancellationToken)
        );

        var inventory = Domain.Inventory.InventoryModel.Register(
            new RegisterDto
            {
                Code = request.Code,
                Name = request.Name,
                Location = request.Location,
                Description = request.Description,
                IsActive = request.IsActive
            }
        );

        await PublishDomainEventsAsync(inventory.GetDomainEventsQueue());

        return inventory.Id;
    }
}