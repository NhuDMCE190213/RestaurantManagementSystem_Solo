using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
        }

        public void Update(Order order, CancellationToken cancellationToken = default)
        {
            _dbContext.Orders.Update(order);
        }

        public async Task DeleteAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            await _dbContext.Orders.Where(o => o.OrderId == orderId).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);
        }
    }
}