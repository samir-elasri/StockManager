using MediatR;

namespace StockManager.Application.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price, Guid UserId) : IRequest<Guid>;
}
