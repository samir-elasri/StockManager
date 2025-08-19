using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StockManager.Application.Products.Queries
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _repo;
        public GetProductsHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken ct)
        {
            return await _repo.Query()
                              .Where(p => p.UserId == request.UserId)
                              .Include(p => p.Variants)
                              .ToListAsync(ct);
        }
    }
}
