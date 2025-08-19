using MediatR;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IRepository<User> _repo;
        public CreateUserCommandHandler(IRepository<User> repo) => _repo = repo;

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var user = new User(request.Email, request.PasswordHash);

            await _repo.AddAsync(user, ct);
            await _repo.SaveChangesAsync(ct);

            return user.Id;
        }
    }
}
