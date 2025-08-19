using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace StockManager.Application.Categories.Commands
{
    public record CreateCategoryCommand(string Name, Guid UserId) : IRequest<Guid>;
}
