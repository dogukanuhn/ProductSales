using ProductSales.Domain.Abstract;
using ProductSales.Domain.Concrete;

namespace ProductSales.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            throw new BusinessRuleValidationException(rule);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
