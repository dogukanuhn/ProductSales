namespace ProductSales.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal BasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal Discount { get; set; }


    }
}
