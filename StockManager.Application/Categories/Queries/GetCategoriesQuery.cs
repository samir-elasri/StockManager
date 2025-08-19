using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using StockManager.Domain.Entities;

namespace StockManager.Application.Categories.Queries
{
    public record GetCategoriesQuery(Guid UserId) : IRequest<IEnumerable<Category>>;
}
