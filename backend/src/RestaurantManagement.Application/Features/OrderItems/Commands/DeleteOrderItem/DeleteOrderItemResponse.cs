using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Features.OrderItems.Commands.DeleteOrderItem
{
    public sealed class DeleteOrderItemResponse
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; }
    }
}
