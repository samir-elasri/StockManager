using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using StockManager.Application.Products.Queries;
using StockManager.Application.Interfaces;
using StockManager.Domain.Entities;

namespace StockManager.Tests.Products
{
    public class GetProductsHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Products_For_User()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var data = new List<Product>
            {
                new Product("A", 1.0m, userId),
                new Product("B", 2.0m, userId),
                new Product("Other", 3.0m, Guid.NewGuid())
            };

            var mockRepo = new Mock<IRepository<Product>>();
            mockRepo.Setup(r => r.Query()).Returns(data.AsQueryable());

            var handler = new GetProductsHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(new GetProductsQuery(userId), CancellationToken.None);

            // Assert
            result.Should().BeAssignableTo<IEnumerable<Product>>();
            result.Should().HaveCount(2);
            result.All(p => p.UserId == userId).Should().BeTrue();
        }
    }
}
