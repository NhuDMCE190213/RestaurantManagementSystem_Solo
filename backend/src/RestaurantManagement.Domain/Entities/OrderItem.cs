using RestaurantManagement.Domain.BaseEntities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; }
        public static OrderItem Create(Guid orderId, Guid menuItemId, int quantity, decimal unitPrice, OrderItemStatus orderItemStatus, Guid employeeId)
        {
            var orderItem = new OrderItem
            {
                OrderItemId = Guid.NewGuid(),
                OrderId = orderId,
                MenuItemId = menuItemId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice,
                OrderItemStatus = orderItemStatus,

                CreatedBy = employeeId,
                CreatedAt = DateTime.UtcNow
            };
            return orderItem;
        }
        public void UpdateOrderItem(int quantity, decimal unitPrice, OrderItemStatus orderItemStatus, Guid employeeId)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
            OrderItemStatus = orderItemStatus;
            UpdatedBy = employeeId;
            UpdatedAt = DateTime.UtcNow;
        }
        public void DeleteOrderItem(Guid employeeId)
        {
            OrderItemStatus = OrderItemStatus.Inactive;
            DeletedBy = employeeId;
            DeletedAt = DateTime.UtcNow;
        }

        public decimal CalculateTotalPrice()
        {
            TotalPrice = Quantity * UnitPrice; // Logic tính toán TotalPrice dựa trên Quantity và UnitPrice
            return TotalPrice;
        }
    }
}
