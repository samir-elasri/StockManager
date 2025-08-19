using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Application.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        string? Photo,
        Guid CategoryId,
        Guid UserId
    ) : IRequest<Guid>;
}
