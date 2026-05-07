using FluentValidation;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemValidator : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemValidator()
        {
            RuleFor(x => x.OrderItemId)
                .NotEmpty().WithMessage("OrderItemId is required.");
        }
    }
}
