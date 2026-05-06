using FluentValidation;

namespace RestaurantManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("OrderId is required.");

            RuleFor(x => x.OrderItems)
                .Must(x => x != null && x.Count > 0)
                .WithMessage("Order must have at least one order item.");

            RuleForEach(x => x.OrderItems).ChildRules(item =>
            {
                item.RuleFor(i => i.MenuItemId)
                    .NotEmpty()
                    .WithMessage("MenuItemId is required.");
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0.");
                item.RuleFor(i => i.UnitPrice)          // Hiện tại chưa có menu nên tạm thời để UnitPrice là bắt buộc, sau này có thể sửa lại khi lấy giá từ menu
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("UnitPrice must be greater than or equal to 0.");
            });
        }
    }
}
