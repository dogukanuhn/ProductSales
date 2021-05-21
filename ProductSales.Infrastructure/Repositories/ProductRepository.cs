using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;

using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDbContext<Product> context) : base(context)
        {
        }
    }
}
