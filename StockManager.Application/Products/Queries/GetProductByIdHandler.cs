using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StockManager.Application.Products.Queries
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IRepository<Product> _repo;
        public GetProductByIdHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            return await _repo.Query()
                              .Include(p => p.Variants)
                              .FirstOrDefaultAsync(p => p.Id == request.Id, ct);
        }
    }
}
