using System;

namespace ProductSales.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Guid Code { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }
}
