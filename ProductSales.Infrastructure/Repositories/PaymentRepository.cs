using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.Models;
using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<CustomerPayment>, IPaymentRepository
    {
        public PaymentRepository(IMongoDbContext<CustomerPayment> context) : base(context)
        {
        }
    }
}
