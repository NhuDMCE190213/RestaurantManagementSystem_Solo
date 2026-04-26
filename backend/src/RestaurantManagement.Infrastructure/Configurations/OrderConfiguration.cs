using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Thiết lập khóa chính
            builder.HasKey(o => o.OrderId);

            // Thiết lập các thuộc tính
            builder.Property(o => o.OrderId);
            builder.Property(o => o.CustomerId);

            // Thiết lập kiểu dữ liệu cho các trường số thập phân
            builder.Property(o => o.Tax).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Discount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            // Trường để quản lý thông tin tạo, cập nhật, xóa
            builder.Property(o => o.CreatedBy).IsRequired();
            builder.Property(o => o.CreatedAt).IsRequired();
            builder.Property(o => o.UpdatedBy);
            builder.Property(o => o.UpdatedAt);
            builder.Property(o => o.DeletedBy);
            builder.Property(o => o.DeletedAt);
        }
    }
}
