using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using System.Threading.Tasks.Dataflow;

namespace RestaurantManagement.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // Tự động áp dụng tất cả các cấu hình từ assembly chứa AppDbContext
        }
    }
}
