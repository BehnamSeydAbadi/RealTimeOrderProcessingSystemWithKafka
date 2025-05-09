using System.Drawing;
using Bogus;
using InventoryService.Domain.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Product;

internal static class ProductSeeder
{
    public static void Seed(this EntityTypeBuilder<ProductEntity> entityTypeBuilder)
    {
        var productEntities = new List<ProductEntity>();

        for (var index = 0; index < 10; index++)
        {
            productEntities.Add(
                new Faker<ProductEntity>()
                    .RuleFor(p => p.Id, Guid.NewGuid())
                    .RuleFor(p => p.Name, f => f.Vehicle.Model())
                    .RuleFor(p => p.StockKeepingUnit, "Count")
                    .RuleFor(
                        p => p.Color,
                        f => f.PickRandom(Color.White, Color.Black, Color.Red, Color.Yellow, Color.Blue)
                            .ToString()
                    ).RuleFor(p => p.UnitOfMeasure, ProductUnitOfMeasure.Pieces)
                    .RuleFor(p => p.IsPerishable, false)
                    .RuleFor(p => p.Dimensions, "250*150 cm")
                    .RuleFor(p => p.Weight, "2500 Kg")
                    .RuleFor(p => p.Category, ProductCategory.Vehicles)
                    .Generate()
            );
        }

        entityTypeBuilder.HasData(productEntities);
    }
}