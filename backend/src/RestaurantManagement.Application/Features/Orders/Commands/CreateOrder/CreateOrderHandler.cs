using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Constants;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.OrderItems is null || request.OrderItems.Count == 0)
            {
                return Result<CreateOrderResponse>.Failure(ErrorCodes.ValidationError, "Order must contain at least one item.");
            }

            // Hiện tại chưa có login nên tạm thời để EmployeeId là bắt buộc, sau này có thể sửa lại khi có hệ thống authentication
            if (request.EmployeeId == Guid.Empty)
            {
                return Result<CreateOrderResponse>.Failure(ErrorCodes.ValidationError, "EmployeeId is required.");
            }

            var order = Order.Create(request.CustomerId, request.TableId, request.EmployeeId);

            foreach (var item in request.OrderItems)
            {
                var orderItem = OrderItem.Create(
                    order.OrderId,
                    item.MenuItemId,
                    item.Quantity,
                    item.UnitPrice,
                    OrderItemStatus.Pending,
                    request.EmployeeId);

                order.OrderItems.Add(orderItem);
            }

            order.CalculateTotalAmount();

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new CreateOrderResponse
            {
                OrderId = order.OrderId,
                TableId = order.TableId,
                CustomerId = order.CustomerId,
                Status = order.OrderStatus.ToString(),
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Discount = order.Discount,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(i => new CreateOrderItemResponse
                {
                    OrderItemId = i.OrderItemId,
                    MenuItemId = i.MenuItemId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice,
                    Status = i.OrderItemStatus.ToString()
                }).ToList()
            };

            return Result<CreateOrderResponse>.Success(response, "Order created successfully.");
        }
    }
}
