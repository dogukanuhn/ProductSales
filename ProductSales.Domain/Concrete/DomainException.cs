using System;

namespace ProductSales.Domain.Concrete
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        protected DomainException()
        {

        }

        protected DomainException(string message) : base(message)
        {

        }
    }
}
