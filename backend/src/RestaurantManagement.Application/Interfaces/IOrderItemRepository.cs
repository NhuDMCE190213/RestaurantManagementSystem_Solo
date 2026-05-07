using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IOrderItemRepository
    {
        void Update(OrderItem orderItem, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid orderItemId, CancellationToken cancellationToken = default);
        Task<OrderItem?> GetByIdAsync(Guid orderItemId, CancellationToken cancellationToken = default);
    }
}
