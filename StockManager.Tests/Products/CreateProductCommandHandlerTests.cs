using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using StockManager.Application.Products.Commands;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Tests.Products
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Product_And_Save()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Product>>();

            // Arrange AddAsync to complete successfully and record invocation
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockRepo
                .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateProductCommandHandler(mockRepo.Object);

            var userId = Guid.NewGuid();
            var cmd = new CreateProductCommand("Widget", 9.99m, userId);

            // Act
            var resultId = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            resultId.Should().NotBe(Guid.Empty);
            mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "Widget" && p.Price == 9.99m && p.UserId == userId), It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
