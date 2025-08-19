using MediatR;

namespace StockManager.Application.Users.Commands
{
    public record CreateUserCommand(
        string Email,
        string PasswordHash
    ) : IRequest<Guid>;
}
