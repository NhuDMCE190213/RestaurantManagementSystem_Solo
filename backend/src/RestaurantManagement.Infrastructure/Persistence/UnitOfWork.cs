using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Infrastructure.Persistence
{
    /// <summary>
    /// Triển khai Unit of Work dựa trên AppDbContext.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}