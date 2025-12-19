using FluentAssertions;
using Moq;
using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Services;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Tests.Orders
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly OrderService _sut;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _sut = new OrderService(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderderAsync_ValidOrderDto_CreatesOrderAndReturnsId()
        {
            // Arrange
            var dto = CreateOrderDtoMother.Valid();
            var cancellationToken = CancellationToken.None;
            var createdOrderId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(r => r.AddAsync(
                    It.IsAny<Order>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _sut.CreateAsync(dto, cancellationToken);

            // Assert
            _orderRepositoryMock.Verify(
                r => r.AddAsync(
                    It.Is<Order>(o =>
                        o.Items.Count == dto.Items.Count &&
                        o.GetTotalAmount().Amount == dto.Items.Sum(i => i.UnitPrice * i.Quantity)
                    ),
                    cancellationToken),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithoutItems_ThrowsException()
        {
            // Arrange
            var dto = CreateOrderDtoMother.WithoutItems();

            // Act
            Func<Task> act = () => _sut.CreateAsync(dto, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("*item*");

            _orderRepositoryMock.Verify(
                r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
