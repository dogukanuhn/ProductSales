using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Concrete;

namespace ProductSales.Domain.Models
{
    public abstract class BaseModel : IEntity
    {


        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }





        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }







    }
}
