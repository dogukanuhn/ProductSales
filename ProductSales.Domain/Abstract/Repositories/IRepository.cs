using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProductSales.Domain.Abstract.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> AddAsync(T entity, CancellationToken token);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity, Expression<Func<T, bool>> predicate);



    }
}
