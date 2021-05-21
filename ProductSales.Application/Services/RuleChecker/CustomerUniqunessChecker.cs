using ProductSales.Domain.Abstract.Repositories;
using ProductSales.Domain.BusinessRules;
using System.Threading.Tasks;

namespace ProductSales.Application.Services.RuleChecker
{
    public class CustomerUniqunessChecker : ICustomerUniqunessChecker
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerUniqunessChecker(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<bool> IsUnique(string email)
        {
            var user = await _customerRepository.GetAsync(x => x.Email == email);
            if (user is null)
                return false;

            return true;
        }
    }
}
