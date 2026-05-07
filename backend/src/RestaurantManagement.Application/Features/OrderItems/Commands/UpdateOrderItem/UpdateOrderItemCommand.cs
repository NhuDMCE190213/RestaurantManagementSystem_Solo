using MediatR;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public sealed record UpdateOrderItemCommand(
        Guid orderItemId,
        int quantity
        ) : IRequest<Result<UpdateOrderItemResponse>>;
}
