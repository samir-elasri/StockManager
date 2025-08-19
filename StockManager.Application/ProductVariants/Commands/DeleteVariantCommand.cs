using MediatR;

namespace StockManager.Application.ProductVariants.Commands
{
    public record DeleteVariantCommand(Guid Id) : IRequest<Unit>;
}
