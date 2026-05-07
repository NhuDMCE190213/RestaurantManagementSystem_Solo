using MediatR;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public sealed record DeleteOrderItemCommand (
        Guid OrderItemId
        ) : IRequest<Result<DeleteOrderItemResponse>>;
}
