using FluentAssertions;
using RestaurantManagement.Application.Features.Orders.Commands.CreateOrder;

namespace RestaurantManagement.UnitTests.Orders.CreateOrder
{
    public class CreateOrderValidatorTests
    {
        [Fact]
        public void Validate_WhenCommandIsValid_ShouldPass()
        {
            // Arrange
            var validator = new CreateOrderValidator();
            var command = new CreateOrderCommand(
                null,
                null,
                Guid.NewGuid(),
                new List<CreateOrderItemCommand>
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
        public void Validate_WhenEmployeeIdIsEmpty_ShouldFail()
        {
            // Arrange
            var validator = new CreateOrderValidator();
            var command = new CreateOrderCommand(
                null,
                null,
                Guid.Empty, // Invalid EmployeeId
                new List<CreateOrderItemCommand>
                {
                    new(Guid.NewGuid(), 1, 10000m)
                });

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateOrderCommand.EmployeeId));
        }

        [Fact]
        public void Validate_WhenOrderItemsIsEmpty_ShouldFail()
        {
            // Arrange
            var validator = new CreateOrderValidator();
            var command = new CreateOrderCommand(
                null,
                null,
                Guid.NewGuid(),
                new List<CreateOrderItemCommand>());

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == nameof(CreateOrderCommand.OrderItems));
        }

        [Fact]
        public void Validate_WhenOrderItemIsInvalid_ShouldFail()
        {
            // Arrange
            var validator = new CreateOrderValidator();
            var command = new CreateOrderCommand(
                null,
                null,
                Guid.NewGuid(),
                new List<CreateOrderItemCommand>
                {
                    new(Guid.Empty, 0, -1m)
                });

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(CreateOrderItemCommand.MenuItemId)));
            result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(CreateOrderItemCommand.Quantity)));
            result.Errors.Should().Contain(e => e.PropertyName.Contains(nameof(CreateOrderItemCommand.UnitPrice)));
        }
    }
}