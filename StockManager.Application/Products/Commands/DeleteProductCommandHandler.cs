using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IRepository<Product> _repo;
        public DeleteProductCommandHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await _repo.GetAsync(request.Id, ct) ?? throw new KeyNotFoundException("Product not found");
            _repo.Remove(product);
            await _repo.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
