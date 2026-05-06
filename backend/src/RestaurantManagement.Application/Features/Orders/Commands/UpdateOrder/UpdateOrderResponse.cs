namespace RestaurantManagement.Application.Features.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderResponse
    {
        public Guid OrderId { get; init; }
        public Guid? TableId { get; init; }
        public Guid? CustomerId { get; init; }
        public string Status { get; init; } = string.Empty;
        public decimal SubTotal { get; init; }
        public decimal Tax { get; init; }
        public decimal Discount { get; init; }
        public decimal TotalAmount { get; init; }
        public List<UpdateOrderItemResponse> UpdatedItems { get; init; } = new();
    }

    public sealed class UpdateOrderItemResponse
    {
        public Guid OrderItemId { get; init; }
        public Guid MenuItemId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
