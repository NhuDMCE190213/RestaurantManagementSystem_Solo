namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Cung cấp điểm commit chung cho nhiều thao tác repository trong cùng một use case.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}