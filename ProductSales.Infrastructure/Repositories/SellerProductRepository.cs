using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure.Repositories
{
    public class SellerProductRepository : Repository<SellerProduct>, ISellerProductRepository
    {
        public SellerProductRepository(IMongoDbContext<SellerProduct> context) : base(context)
        {
        }
    }
}
