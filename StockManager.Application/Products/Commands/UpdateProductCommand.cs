using MediatR;

namespace StockManager.Application.Products.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        decimal Price
    ) : IRequest<Unit>;
}
