using MediatR;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Queries
{
    public record GetVariantsQuery(Guid ProductId) : IRequest<IEnumerable<ProductVariant>>;
}
