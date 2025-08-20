using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IRepository<Product> _repo;
        public CreateProductCommandHandler(IRepository<Product> repo) => _repo = repo;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Price, request.UserId);
            await _repo.AddAsync(product, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
