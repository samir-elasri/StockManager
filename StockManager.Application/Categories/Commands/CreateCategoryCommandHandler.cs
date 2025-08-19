using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IRepository<Category> _repo;
        public CreateCategoryCommandHandler(IRepository<Category> repo) => _repo = repo;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name, request.UserId);
            await _repo.AddAsync(category, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}
