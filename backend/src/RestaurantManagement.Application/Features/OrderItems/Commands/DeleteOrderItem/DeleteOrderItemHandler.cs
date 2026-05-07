using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Constants.ErrorCodes;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemHandler : IRequestHandler<DeleteOrderItemCommand, Result<DeleteOrderItemResponse>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderItemHandler(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeleteOrderItemResponse>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(request.OrderItemId, cancellationToken);
            if (orderItem == null)
            {
                return Result<DeleteOrderItemResponse>.Failure(OrderItemErrorCodes.NotFound, $"Order Item with ID {request.OrderItemId} not found.");
            }

            if (!orderItem.CanDelete())
            {
                return Result<DeleteOrderItemResponse>.Failure(OrderItemErrorCodes.StatusNotAllowToDelete, $"Order Item with ID {request.OrderItemId} cannot be deleted due to its current status.");
            }

            await _orderItemRepository.DeleteAsync(orderItem.OrderItemId, cancellationToken );
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new DeleteOrderItemResponse
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                MenuItemId = orderItem.MenuItemId,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice,
                OrderItemStatus = orderItem.OrderItemStatus
            };

            return Result<DeleteOrderItemResponse>.Success(response, $"Order Item with ID {orderItem.OrderItemId} deleted successfully.");
        }
    }
}
