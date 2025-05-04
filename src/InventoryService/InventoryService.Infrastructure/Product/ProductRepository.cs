using InventoryService.Domain.Product;
using InventoryService.Infrastructure.Common;

namespace InventoryService.Infrastructure.Product;

public class ProductRepository : AbstractRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(InventoryServiceDbContext dbContext) : base(dbContext)
    {
    }
}