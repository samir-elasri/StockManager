using MediatR;
using StockManager.Domain.Entities;

namespace StockManager.Application.ProductVariants.Queries
{
    public record GetVariantByIdQuery(Guid Id) : IRequest<ProductVariant?>;
}
