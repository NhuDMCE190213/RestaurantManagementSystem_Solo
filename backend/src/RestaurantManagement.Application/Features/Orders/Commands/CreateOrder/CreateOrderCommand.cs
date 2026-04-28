using MediatR;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Features.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(
        Guid? TableId,
        Guid? CustomerId,
        Guid EmployeeId,        // Hiện tại chưa có login nên tạm thời để EmployeeId là bắt buộc, sau này có thể sửa lại khi có hệ thống authentication
        List<CreateOrderItemCommand> OrderItems
    ) : IRequest<Result<CreateOrderResponse>>;

    public sealed record CreateOrderItemCommand(
        Guid MenuItemId,
        int Quantity,
        decimal UnitPrice       // Hiện tại chưa có menu nên tạm thời để UnitPrice là bắt buộc, sau này có thể sửa lại khi lấy giá từ menu
    );
}
