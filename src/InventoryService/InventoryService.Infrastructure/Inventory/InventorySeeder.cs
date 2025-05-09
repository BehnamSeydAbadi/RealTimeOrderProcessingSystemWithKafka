using Bogus;
using Bogus.Extensions.UnitedStates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Inventory;

internal static class InventorySeeder
{
    public static void Seed(this EntityTypeBuilder<InventoryEntity> entityTypeBuilder)
    {
        var inventoryEntities = new List<InventoryEntity>();

        for (var index = 0; index < 10; index++)
        {
            inventoryEntities.Add(
                new Faker<InventoryEntity>()
                    .RuleFor(i => i.Id, f => f.Random.Guid())
                    .RuleFor(i => i.Code, f => f.Company.Ein())
                    .RuleFor(i => i.Name, f => f.Company.CompanyName())
                    .RuleFor(i => i.Location, f => f.Address.FullAddress())
                    .RuleFor(i => i.Description, f => f.Lorem.Sentence())
                    .RuleFor(i => i.IsActive, f => f.Random.Bool())
                    .RuleFor(
                        i => i.RegisteredAt,
                        f => f.Date.Between(start: new DateTime(2010, 1, 1), end: new DateTime(2025, 1, 1))
                    ).Generate()
            );
        }

        entityTypeBuilder.HasData(inventoryEntities);
    }
}