using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity);
        IQueryable<T> Query();
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
