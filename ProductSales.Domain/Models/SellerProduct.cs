using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Domain.ValueObjects;
using System;

namespace ProductSales.Domain.Models
{
    public class SellerProduct : BaseModel
    {
        [BsonRepresentation(BsonType.String)]
        public Guid SellerCode { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public Price Pricing { get; set; }
        public string Sku { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; }

        public SellerProduct()
        {

        }
        public SellerProduct(string sellerCode, string productName, int Qty, string categoryName, Price pricing, string unit, string sku)
        {
            ProductName = productName;
            this.Qty = Qty;
            Pricing = pricing;

            Unit = unit;
            IsActive = true;
            IsDeleted = false;
            CategoryName = categoryName;


            SellerCode = Guid.Parse(sellerCode);
            Sku = sku;
        }





        public void DeleteProduct()
        {
            IsActive = false;
            IsDeleted = true;

        }

        public void DeactivateProduct()
        {
            IsActive = false;

        }
    }
}
