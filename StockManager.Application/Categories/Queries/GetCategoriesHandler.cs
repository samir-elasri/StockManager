using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Categories.Queries
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly IRepository<Category> _repo;
        public GetCategoriesHandler(IRepository<Category> repo) => _repo = repo;

        public Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var items = _repo.Query().Where(c => c.UserId == request.UserId).AsEnumerable();
            return Task.FromResult(items);
        }
    }
}
