using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Commands
{
    public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand, Unit>
    {
        private readonly IRepository<ProductVariant> _repo;
        public DeleteVariantCommandHandler(IRepository<ProductVariant> repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteVariantCommand request, CancellationToken ct)
        {
            var variant = await _repo.GetAsync(request.Id, ct) ?? throw new KeyNotFoundException("Variant not found");
            _repo.Remove(variant);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
