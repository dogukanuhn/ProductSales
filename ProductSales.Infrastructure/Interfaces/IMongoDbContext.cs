using MongoDB.Driver;

namespace ProductSales.Infrastructure.Interfaces
{
    public interface IMongoDbContext<T>
    {
        IMongoCollection<T> GetCollection();
    }
}
