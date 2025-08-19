using MediatR;
using StockManager.Domain.Entities;

namespace StockManager.Application.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;
}
