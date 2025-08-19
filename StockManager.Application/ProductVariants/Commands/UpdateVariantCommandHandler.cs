using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Commands
{
    public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, Unit>
    {
        private readonly IRepository<ProductVariant> _repo;
        public UpdateVariantCommandHandler(IRepository<ProductVariant> repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateVariantCommand request, CancellationToken ct)
        {
            var variant = await _repo.GetAsync(request.Id, ct) ?? throw new KeyNotFoundException("Variant not found");
            typeof(ProductVariant).GetProperty("Name")?.SetValue(variant, request.Name);
            typeof(ProductVariant).GetProperty("Price")?.SetValue(variant, request.Price);
            typeof(ProductVariant).GetProperty("Photo")?.SetValue(variant, request.Photo);

            _repo.Update(variant);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
