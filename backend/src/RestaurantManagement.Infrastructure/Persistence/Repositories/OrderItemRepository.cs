using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Repositories
{
    public sealed class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            await _dbContext.OrderItems.Where(oi => oi.OrderItemId == orderItemId).ExecuteDeleteAsync(cancellationToken);
        }

        public void Update(OrderItem orderItem, CancellationToken cancellationToken = default)
        {
            _dbContext.OrderItems.Update(orderItem);
        }

        public async Task<OrderItem?> GetByIdAsync(Guid orderItemId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == orderItemId, cancellationToken);
        }
    }
}
