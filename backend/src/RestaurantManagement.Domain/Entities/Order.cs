using RestaurantManagement.Domain.BaseEntities;

namespace RestaurantManagement.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid? CustomerId { get; set; }   // Có thể null nếu nhân viên tạo đơn hàng cho khách vãng lai
        public double Tax { get; set; }
        public double Discount { get; set; }
        public double SubTotal { get; set; }
        public double TotalAmount { get; set; }

        public Order AddOrder(Guid? customerId, Guid employeeId)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customerId,
                CreatedBy = employeeId,
                CreatedAt = DateTime.UtcNow
            };
            return order;
        }

        public void UpdateOrder(Guid? customerId, Guid employeeId)
        {
            CustomerId = customerId;
            UpdatedBy = employeeId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteOrder(Guid employeeId)
        { 
            UpdatedBy = employeeId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CalculateTax()
        {
            Tax = SubTotal * 0.1; // Ví dụ tính thuế 10% trên SubTotal
        }
        public void CalculateSubTotal()
        {
            SubTotal = 0; // Logic tính toán SubTotal dựa trên các OrderItem sẽ được thêm vào sau
        }
        public void CalculateDiscount()
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
    }
}
