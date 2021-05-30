using ProductSales.Domain.Models;
using System.Threading.Tasks;

namespace ProductSales.Domain.Abstract.Services
{
    public interface IPaymentService
    {
        Task Create(CustomerPayment customerPayment);
    }
}
