using FluentAssertions;
using RestaurantManagement.Application.Features.Orders.Commands.UpdateOrder;

namespace RestaurantManagement.UnitTests.Orders.UpdateOrder
{
    public class UpdateOrderValidatorTest
    {
        [Fact]
        public void Validate_WhenCommandIsValid_ShouldPass()
        {
            // Arrange
            var validator = new UpdateOrderValidator();
            var command = new UpdateOrderCommand(
                OrderId: Guid.NewGuid(),
                OrderItems: new List<UpdateOrderItemCommand>
                {
                    new(Guid.NewGuid(), 2, 15000m)
                });

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_WhenOrderIdIsEmpty_ShouldFail()
        {
            // Arrange
            var validator = new UpdateOrderValidator();
            var command = new UpdateOrderCommand(
                OrderId: Guid.Empty, // Invalid OrderId
                OrderItems: new List<UpdateOrderItemCommand>
                {
                    new(Guid.NewGuid(), 1, 10000m)
                });
            // Act
            var result = validator.Validate(command);
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(UpdateOrderCommand.OrderId));
        }

        [Fact]
        public void Validate_WhenOrderItemsIsEmpty_ShouldFail()
        {
            // Arrange
            var validator = new UpdateOrderValidator();
            var command = new UpdateOrderCommand(
                OrderId: Guid.NewGuid(),
                OrderItems: new List<UpdateOrderItemCommand>());
            // Act
            var result = validator.Validate(command);
            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(UpdateOrderCommand.OrderItems));
        }

        [Fact]
        public void Validate_WhenOrderItemHasInvalidMenuItemId_ShouldFail()
        {
            // Arrange
            var validator = new UpdateOrderValidator();
            var command = new UpdateOrderCommand(
                OrderId: Guid.NewGuid(),
                OrderItems: new List<UpdateOrderItemCommand>
                {
                    new(Guid.Empty, 0, -1m) // Invalid MenuItemId
                });
            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "OrderItems[0].MenuItemId");
            result.Errors.Should().ContainSingle(e => e.PropertyName == "OrderItems[0].Quantity");
            result.Errors.Should().ContainSingle(e => e.PropertyName == "OrderItems[0].UnitPrice");
        }
    }
}
