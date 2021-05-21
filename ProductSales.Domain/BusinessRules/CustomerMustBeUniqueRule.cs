using ProductSales.Domain.Abstract;

namespace ProductSales.Domain.BusinessRules
{
    public class CustomerMustBeUniqueRule : IBusinessRule
    {
        private readonly ICustomerUniqunessChecker _customerUniqunessChecker;

        private readonly string _email;
        public CustomerMustBeUniqueRule(ICustomerUniqunessChecker customerUniqunessChecker, string email)
        {
            _customerUniqunessChecker = customerUniqunessChecker;
            _email = email;
        }
        public string Message => "Customer already registered.";

        public bool IsBroken() => _customerUniqunessChecker.IsUnique(_email).Result;

    }
}
