using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        void Update(Order order, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task<List<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}