using MediatR;

namespace StockManager.Application.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Unit>;
}
