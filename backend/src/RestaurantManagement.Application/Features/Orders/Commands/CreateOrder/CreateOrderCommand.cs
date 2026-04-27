using MediatR;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Features.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand (
        Guid? tableId,
        Guid? customerId,
        List<CreateOrderItemCommand> orderItems
        ) : IRequest<Result<CreateOrderResponse>>;

    public sealed record CreateOrderItemCommand(
        Guid menuItemId,
        int quantity
    ) : IRequest<Result<CreateOrderItemResponse>>;
}
