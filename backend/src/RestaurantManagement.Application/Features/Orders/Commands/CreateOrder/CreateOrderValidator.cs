using FluentValidation;

namespace RestaurantManagement.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.EmployeeId)          // Hiện tại chưa có login nên tạm thời để EmployeeId là bắt buộc, sau này có thể sửa lại khi có hệ thống authentication
                .NotEmpty()
                .WithMessage("EmployeeId is required.");        

            RuleFor(x => x.OrderItems)
                .NotNull()
                .WithMessage("OrderItems is required.")
                .Must(x => x != null && x.Count > 0)
                .WithMessage("Order must contain at least one item.");

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
