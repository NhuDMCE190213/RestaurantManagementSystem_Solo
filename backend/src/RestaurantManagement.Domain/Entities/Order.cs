using RestaurantManagement.Domain.BaseEntities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid? CustomerId { get; set; }   // Có thể null nếu nhân viên tạo đơn hàng cho khách vãng lai
        public Guid? TableId { get; set; }      // Có thể null nếu đơn hàng mang đi
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Quan hệ một-nhiều với OrderItem
        public virtual Table? Table { get; set; } // Quan hệ nhiều-một với Table

        public static Order Create(Guid? customerId, Guid? tableId, Guid employeeId)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customerId,
                TableId = tableId,
                OrderStatus = OrderStatus.Pending,
                CreatedBy = employeeId,
                CreatedAt = DateTime.UtcNow
            };
            return order;
        }

        public void UpdateOrder(Guid? customerId, Guid? tableId, OrderStatus orderStatus, Guid employeeId)
        {
            CustomerId = customerId;
            TableId = tableId;
            OrderStatus = orderStatus;
            UpdatedBy = employeeId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteOrder(Guid employeeId)
        {
            OrderStatus = OrderStatus.Inactive;
            DeletedBy = employeeId;
            DeletedAt = DateTime.UtcNow;
        }


        /// <summary>
        /// Phương thức này sẽ được gọi mỗi khi có sự thay đổi liên quan đến OrderItems (thêm, sửa, xóa)
        /// \để tự động cập nhật lại các giá trị Tax, Discount, SubTotal và TotalAmount của đơn hàng.
        /// </summary>
        private void CalculateTax()
        {
            Tax = SubTotal * Constants.TaxConstants.DefaultVatRate; // Tính thuế dựa trên hằng số
        }
        /// <summary>
        /// Phương thức này sẽ được gọi mỗi khi có sự thay đổi liên quan đến OrderItems (thêm, sửa, xóa)
        /// </summary>
        private void CalculateSubTotal()
        {
            SubTotal = OrderItems.Sum(item => item.CalculateTotalPrice()); // Logic tính toán SubTotal dựa trên các OrderItem
        }
        /// <summary>
        /// Phương thức này sẽ được gọi mỗi khi có sự thay đổi liên quan đến OrderItems (thêm, sửa, xóa)
        /// </summary>
        private void CalculateDiscount()
        {
            Discount = 0; // Logic tính toán giảm giá dựa trên các chương trình khuyến mãi hoặc mã giảm giá
        }
        public void CalculateTotalAmount()
        {
            CalculateSubTotal();
            CalculateTax();
            CalculateDiscount();

            TotalAmount = SubTotal + Tax - Discount;
        }

        public void AddOrUpdateOrderItem(Guid menuItemId, int quantity, decimal unitPrice, OrderItemStatus status, Guid employeeId)
        {
            var existingItem = OrderItems.FirstOrDefault(i => i.MenuItemId == menuItemId && i.OrderItemStatus == OrderItemStatus.Pending);
            if (existingItem != null)
            {
                existingItem.UpdateOrderItem(quantity, unitPrice, status, employeeId);
            }
            else
            {
                var newItem = OrderItem.Create(OrderId, menuItemId, quantity, unitPrice, status, employeeId);
                OrderItems.Add(newItem);
            }
            CalculateTotalAmount(); // Cập nhật lại tổng tiền sau khi thêm/sửa OrderItem
        }
    }
}