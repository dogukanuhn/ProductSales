using ProductSales.Domain.Models;

namespace ProductSales.Domain.Abstract.Repositories
{
    public interface IPaymentRepository : IRepository<CustomerPayment>
    {
    }
}
