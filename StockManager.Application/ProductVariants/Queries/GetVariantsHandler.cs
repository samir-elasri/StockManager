using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Queries
{
    public class GetVariantsHandler : IRequestHandler<GetVariantsQuery, IEnumerable<ProductVariant>>
    {
        private readonly IRepository<ProductVariant> _repo;
        public GetVariantsHandler(IRepository<ProductVariant> repo) => _repo = repo;

        public async Task<IEnumerable<ProductVariant>> Handle(GetVariantsQuery request, CancellationToken ct)
        {
            return await _repo.Query().Where(v => v.ProductId == request.ProductId).ToListAsync(ct);
        }
    }
}
