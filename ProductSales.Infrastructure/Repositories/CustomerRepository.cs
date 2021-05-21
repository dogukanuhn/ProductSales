using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IMongoDbContext<Customer> context) : base(context)
        {
        }
    }
}
