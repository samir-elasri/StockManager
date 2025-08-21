using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using StockManager.Application.Users.Commands;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Tests.Users
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_User_And_Save()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<User>>();

            // Arrange AddAsync to complete successfully and record invocation
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockRepo
                .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateUserCommandHandler(mockRepo.Object);

            var email = "test@local";
            var passwordHash = "hash";

            var cmd = new CreateUserCommand(email, passwordHash);

            // Act
            var resultId = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            resultId.Should().NotBe(Guid.Empty);
            mockRepo.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == email && u.PasswordHash == passwordHash), It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
