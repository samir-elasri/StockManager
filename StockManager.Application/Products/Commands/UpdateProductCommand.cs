using MediatR;

namespace StockManager.Application.Products.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        decimal Price,
        string? Photo
    ) : IRequest<Unit>;
}
