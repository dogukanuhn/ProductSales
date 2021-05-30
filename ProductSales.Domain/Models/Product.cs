using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Domain.ValueObjects;
using System;

namespace ProductSales.Domain.Models
{
    public class Product : BaseModel
    {

        [BsonRepresentation(BsonType.String)]
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public Price Pricing { get; set; }
        public string Sku { get; set; }
        public string Unit { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string CategoryName { get; set; }

        public Product()
        {

        }
        public Product(string productName, short Qty, string categoryName, Price pricing, string unit)
        {
            ProductName = productName;
            this.Qty = Qty;
            Pricing = pricing;
            Unit = unit;
            IsActive = true;
            IsDeleted = false;
            CategoryName = categoryName;

            SkuGenerator();
        }



        private void SkuGenerator()
        {
            var tempGuid = Guid.NewGuid().ToString();
            var sku = $"{ProductName.Substring(0, 3)}-{CategoryName.Substring(0, 3)}-{tempGuid.Substring(tempGuid.Length - 6)}";
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
