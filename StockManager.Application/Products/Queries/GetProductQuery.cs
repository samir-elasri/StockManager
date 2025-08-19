using MediatR;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Queries
{
    public record GetProductsQuery(Guid UserId) : IRequest<IEnumerable<Product>>;
}
