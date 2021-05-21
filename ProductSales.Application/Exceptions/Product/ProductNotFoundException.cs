
using ProductSales.Domain.Exceptions;
using System;


namespace ProductSales.Application.Exceptions
{
    [Serializable]
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}
