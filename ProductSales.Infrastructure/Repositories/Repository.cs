using MongoDB.Driver;
using ProductSales.Domain.Abstract;
using ProductSales.Domain.Abstract.Repositories;

using ProductSales.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {

        private readonly IMongoDbContext<T> _context;
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoDbContext<T> context)
        {
            _context = context;
            _collection = _context.GetCollection();

        }

        public async Task<T> AddAsync(T entity, CancellationToken token)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await _collection.InsertOneAsync(entity, options, token);
            return entity;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _collection.Find(_ => true).ToListAsync() : await _collection.Find(predicate).ToListAsync();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {

            await _collection.FindOneAndReplaceAsync<T>(predicate, entity);

        }
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {

            await _collection.FindOneAndDeleteAsync<T>(predicate);

        }


    }
}
