using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IRepository<Product> _repo;
        public CreateProductCommandHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var product = new Product(request.Name, request.Price, request.CategoryId, request.UserId, request.Photo);
            await _repo.AddAsync(product, ct);
            await _repo.SaveChangesAsync(ct);
            return product.Id;
        }
    }
}
