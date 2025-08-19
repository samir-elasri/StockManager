using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Commands
{
    public class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, Guid>
    {
        private readonly IRepository<ProductVariant> _repo;
        public CreateVariantCommandHandler(IRepository<ProductVariant> repo) => _repo = repo;

        public async Task<Guid> Handle(CreateVariantCommand request, CancellationToken ct)
        {
            var variant = new ProductVariant(request.Name, request.Price, request.ProductId, request.Photo);
            await _repo.AddAsync(variant, ct);
            await _repo.SaveChangesAsync(ct);
            return variant.Id;
        }
    }
}
