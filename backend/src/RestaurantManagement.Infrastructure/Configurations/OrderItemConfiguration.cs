using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Cấu hình tên bảng
            builder.ToTable("OrderItems");

            // Cấu hình khóa chính và các thuộc tính
            builder.HasKey(oi => oi.OrderItemId);
            builder.Property(oi => oi.OrderItemId).ValueGeneratedNever();

            // Cấu hình các id của Order và MenuItem
            builder.Property(oi => oi.OrderId).IsRequired();
            builder.Property(oi => oi.MenuItemId).IsRequired();

            // Cấu hình số lượng với giá trị mặc định là 1
            builder.Property(oi => oi.Quantity)
                .IsRequired()
                .HasDefaultValue(1);

            // Cấu hình các thuộc tính tiền tệ
            builder.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();
            builder.Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();

            // Cấu hình enum OrderItemStatus
            builder.Property(oi => oi.OrderItemStatus)
                .HasConversion<string>()
                .HasDefaultValue(OrderItemStatus.Pending)
                .HasMaxLength(50)
                .IsRequired();

            // Cấu hình các thuộc tính của BaseEntity
            builder.Property(oi => oi.CreatedBy).IsRequired();
            builder.Property(oi => oi.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(oi => oi.UpdatedBy).IsRequired(false);
            builder.Property(oi => oi.UpdatedAt).IsRequired(false);
            builder.Property(oi => oi.DeletedBy).IsRequired(false);
            builder.Property(oi => oi.DeletedAt).IsRequired(false);

            // Cấu hình filter để chỉ lấy các OrderItem chưa bị xóa
            builder.HasQueryFilter(oi => oi.DeletedAt == null);

            // Cấu hình index để tối ưu hóa hiệu suất truy vấn
            builder.HasIndex(oi => oi.OrderId);
            builder.HasIndex(oi => oi.MenuItemId);
            builder.HasIndex(oi => oi.OrderItemStatus);
            builder.HasIndex(oi => oi.CreatedAt);
            builder.HasIndex(oi => new { oi.OrderItemStatus, oi.CreatedAt });

            // Cấu hình các ràng buộc đảm bảo tính nhất của dữ liệu
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("ck_order_items_quantity_non_negative", "quantity >= 0");
                table.HasCheckConstraint("ck_order_items_unit_price_non_negative", "unit_price >= 0");
                table.HasCheckConstraint("ck_order_items_total_price_non_negative", "total_price >= 0");
                table.HasCheckConstraint("ck_order_items_total_price_consistent", "total_price = quantity * unit_price");
            }); 
        }
    }
}
