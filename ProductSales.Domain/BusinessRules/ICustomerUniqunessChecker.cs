using System.Threading.Tasks;

namespace ProductSales.Domain.BusinessRules
{
    public interface ICustomerUniqunessChecker
    {
        Task<bool> IsUnique(string email);
    }
}
