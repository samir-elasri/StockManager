using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Queries
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _repo;
        public GetProductsHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.Query()
                              .Where(p => p.UserId == request.UserId)
                              .ToListAsync(cancellationToken);
        }
    }
}
