using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
        {
            await context.Database.MigrateAsync(cancellationToken);

            if (await context.Orders.AnyAsync(cancellationToken))
            {
                return;
            }

            var now = DateTime.UtcNow;

            var sampleOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = null,
                SubTotal = 100000,
                Tax = 10000,
                Discount = 0,
                TotalAmount = 110000,
                CreatedBy = Guid.Empty,
                CreatedAt = now
            };

            context.Orders.Add(sampleOrder);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}