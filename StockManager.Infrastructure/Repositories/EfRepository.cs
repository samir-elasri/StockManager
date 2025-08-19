using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly StockDbContext _context;
        public EfRepository(StockDbContext context) => _context = context;
        public IQueryable<T> Query() => _context.Set<T>().AsQueryable();
        public async Task<T?> GetAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, ct);
        }
        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _context.Set<T>().AddAsync(entity, ct);
        }
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Remove(T entity) => _context.Set<T>().Remove(entity);
        public Task SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
