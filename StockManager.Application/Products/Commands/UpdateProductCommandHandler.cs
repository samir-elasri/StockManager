using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IRepository<Product> _repo;
        public UpdateProductCommandHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var product = await _repo.GetAsync(request.Id, ct) ?? throw new KeyNotFoundException("Product not found");

            product.UpdatePrice(request.Price);
            // manually update Name/Photo since we made only UpdatePrice method in entity
            typeof(Product).GetProperty("Name")?.SetValue(product, request.Name);
            typeof(Product).GetProperty("Photo")?.SetValue(product, request.Photo);

            _repo.Update(product);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
