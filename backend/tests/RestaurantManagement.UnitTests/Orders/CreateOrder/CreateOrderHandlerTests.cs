using FluentAssertions;
using Moq;
using RestaurantManagement.Application.Features.Orders.Commands.CreateOrder;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.UnitTests.Orders.CreateOrder
{
    public class CreateOrderHandlerTests
    {
        [Fact]
        public async Task Handle_WhenValidCommand_ShouldCreateOrder()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            orderRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWork
                .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new CreateOrderHandler(orderRepository.Object, unitOfWork.Object);

            var command = new CreateOrderCommand(
                TableId: null,
                CustomerId: null,
                EmployeeId: Guid.NewGuid(),
                OrderItems: new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand(
                        MenuItemId: Guid.NewGuid(),
                        Quantity: 2,
                        UnitPrice: 10.0m
                        )
                }
            );

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
        }
    }
}
