using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductSales.Infrastructure.Config;
using ProductSales.Infrastructure.Interfaces;

namespace ProductSales.Infrastructure
{
    public class DbContext<T> : IMongoDbContext<T>
    {
        protected readonly IMongoCollection<T> Collection;
        private readonly MongoDbSettings settings;
        private IMongoDatabase Db { get; set; }
        public DbContext(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            Db = client.GetDatabase(this.settings.Database);
        }
      



        public IMongoCollection<T> GetCollection()
        {
            return Db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }
    }
}
