using MediatR;

namespace StockManager.Application.ProductVariants.Commands
{
    public record CreateVariantCommand(
        string Name,
        decimal Price,
        string? Photo,
        Guid ProductId
    ) : IRequest<Guid>;
}
