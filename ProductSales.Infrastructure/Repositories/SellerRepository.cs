using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure.Repositories
{
    public class SellerRepository : Repository<Seller>, ISellerRepository
    {
        public SellerRepository(IMongoDbContext<Seller> context) : base(context)
        {
        }
    }
}
