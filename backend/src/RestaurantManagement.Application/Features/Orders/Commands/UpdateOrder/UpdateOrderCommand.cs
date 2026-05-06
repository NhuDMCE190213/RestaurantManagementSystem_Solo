using MediatR;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(
        Guid OrderId,
        List<UpdateOrderItemCommand> OrderItems
        ) : IRequest<Result<UpdateOrderResponse>>;

    public record UpdateOrderItemCommand(
        Guid MenuItemId,
        int Quantity,
        decimal UnitPrice
    );
}
