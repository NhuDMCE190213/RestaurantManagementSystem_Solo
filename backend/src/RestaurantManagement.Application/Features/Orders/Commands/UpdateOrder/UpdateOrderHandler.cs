using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Constants.ErrorCodes;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<UpdateOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdateOrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
            {
                return Result<UpdateOrderResponse>.Failure(OrderErrorCodes.OrderNotFound, "Order not found.");
            }

            if (!order.CanUpdateOrderItems())
            {
                return Result<UpdateOrderResponse>.Failure(OrderErrorCodes.OrderStatusNotAllowToUpdate, "Order status does not allow updating order items.");
            }

            foreach (var item in request.OrderItems)
            {
                order.AddOrUpdateOrderItem(item.MenuItemId, item.Quantity, item.UnitPrice, OrderItemStatus.Pending, Guid.Empty);
            }

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new UpdateOrderResponse
            {
                OrderId = order.OrderId,
                TableId = order.TableId,
                CustomerId = order.CustomerId,
                Status = order.OrderStatus.ToString(),
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Discount = order.Discount,
                TotalAmount = order.TotalAmount,
                UpdatedItems = order.OrderItems.Select(oi => new UpdateOrderItemResponse
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Status = oi.OrderItemStatus.ToString()
                }).ToList()
            };

            return Result<UpdateOrderResponse>.Success(response, "Order updated successfully.");
        }
    }
}
