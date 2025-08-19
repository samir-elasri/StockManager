using MediatR;

namespace StockManager.Application.ProductVariants.Commands
{
    public record UpdateVariantCommand(
        Guid Id,
        string Name,
        decimal Price,
        string? Photo
    ) : IRequest<Unit>;
}
