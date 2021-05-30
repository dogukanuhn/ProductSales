using ProductSales.Domain.ValueObjects;

namespace ProductSales.Application.Dtos
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string Sku { get; set; }
        public Price Pricing { get; set; }
        public string Unit { get; set; }
        public string CategoryName { get; set; }
    }
}
