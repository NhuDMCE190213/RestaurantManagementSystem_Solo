using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Constants.ErrorCodes;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemHandler : IRequestHandler<UpdateOrderItemCommand, Result<UpdateOrderItemResponse>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;   

        public UpdateOrderItemHandler(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateOrderItemResponse>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(request.orderItemId);
            if (orderItem == null)
            {
                return Result<UpdateOrderItemResponse>.Failure(OrderItemErrorCodes.NotFound, $"Order item with ID {request.orderItemId} not found.");
            }

            if (!orderItem.CanUpdate())
            {
                return Result<UpdateOrderItemResponse>.Failure(OrderItemErrorCodes.StatusNotAllowToUpdate, $"Order item with ID {request.orderItemId} cannot be updated due to its current status.");
            }

            orderItem.UpdateOrderItem(request.quantity, Guid.Empty);
            await _unitOfWork.SaveChangesAsync();

            var response = new UpdateOrderItemResponse
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                MenuItemId = orderItem.MenuItemId,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice,
                OrderItemStatus = orderItem.OrderItemStatus
            };

            return Result<UpdateOrderItemResponse>.Success(response);
        }
    }
}
