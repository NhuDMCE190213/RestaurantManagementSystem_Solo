using FluentValidation;

namespace RestaurantManagement.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator() { 
            RuleFor(x => x.tableId).NotEmpty().WithMessage("Table ID is required."); // Test
        }
    }
}
