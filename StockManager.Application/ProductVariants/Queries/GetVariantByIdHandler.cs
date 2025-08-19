using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Queries
{
    public class GetVariantByIdHandler : IRequestHandler<GetVariantByIdQuery, ProductVariant?>
    {
        private readonly IRepository<ProductVariant> _repo;
        public GetVariantByIdHandler(IRepository<ProductVariant> repo) => _repo = repo;

        public async Task<ProductVariant?> Handle(GetVariantByIdQuery request, CancellationToken ct)
        {
            return await _repo.Query().FirstOrDefaultAsync(v => v.Id == request.Id, ct);
        }
    }
}
