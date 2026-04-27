using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
        {
            await context.Database.MigrateAsync(cancellationToken);

            if (!await context.Orders.AnyAsync(cancellationToken))
            {
                var now = DateTime.UtcNow;

                var table = new Table
                {
                    TableId = Guid.NewGuid(),
                    TableNumber = 1,
                    Capacity = 4,
                    TableStatus = TableStatus.Available,

                    CreatedBy = Guid.Empty,
                    CreatedAt = now
                };

                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerId = null,
                    TableId = table.TableId,
                    SubTotal = 100000,
                    Tax = 10000,
                    Discount = 0,
                    TotalAmount = 110000,
                    OrderStatus = OrderStatus.Pending,
                    CreatedBy = Guid.Empty,
                    CreatedAt = now
                };

                var orderItem01 = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    MenuItemId = Guid.NewGuid(),
                    Quantity = 2,
                    UnitPrice = 50000,
                    TotalPrice = 100000,

                    CreatedBy = Guid.Empty,
                    CreatedAt = now
                };

                var orderItem02 = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    MenuItemId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitPrice = 50000,
                    TotalPrice = 50000,

                    CreatedBy = Guid.Empty,
                    CreatedAt = now
                };
                
                var orderItem03 = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    MenuItemId = Guid.NewGuid(),
                    Quantity = 1,
                    UnitPrice = 50000,
                    TotalPrice = 50000,

                    CreatedBy = Guid.Empty,
                    CreatedAt = now
                };

                context.Tables.Add(table);
                context.Orders.Add(order);
                context.OrderItems.AddRange(orderItem01, orderItem02, orderItem03);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}