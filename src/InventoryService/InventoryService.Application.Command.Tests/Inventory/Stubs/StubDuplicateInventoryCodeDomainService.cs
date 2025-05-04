using InventoryService.Domain.DomainService;

namespace InventoryService.Application.Command.Tests.Inventory.Stubs;

public class StubDuplicateInventoryCodeDomainService : IDuplicateInventoryCodeDomainService
{
    private bool _isInventoryCodeDuplicate;

    private StubDuplicateInventoryCodeDomainService()
    {
    }

    public static StubDuplicateInventoryCodeDomainService New() => new();

    public StubDuplicateInventoryCodeDomainService WithIsInventoryCodeDuplicateValue(bool isInventoryCodeDuplicate)
    {
        _isInventoryCodeDuplicate = isInventoryCodeDuplicate;
        return this;
    }

    public async Task<bool> IsInventoryCodeDuplicateAsync(string code)
    {
        await Task.CompletedTask;
        return _isInventoryCodeDuplicate;
    }
}