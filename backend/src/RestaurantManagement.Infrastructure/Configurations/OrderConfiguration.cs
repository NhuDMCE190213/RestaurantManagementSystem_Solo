using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Cấu hình tên bảng
            builder.ToTable("Orders");

            // Cấu hình khóa chính và đảm bảo rằng OrderId không được tự động sinh ra
            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.OrderId).ValueGeneratedNever();

            // Cấu hình các trường CustomerId và TableId cho phép null, vì một đơn hàng có thể không liên quan đến khách hàng hoặc bàn cụ thể nào
            builder.Property(o => o.CustomerId).IsRequired(false);
            builder.Property(o => o.TableId).IsRequired(false);

            // Cấu hình các thuộc tính tiền tệ với kiểu dữ liệu decimal và giá trị mặc định là 0
            builder.Property(o => o.Tax)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();
            builder.Property(o => o.Discount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();
            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .IsRequired();

            // Cấu hình thuộc tính OrderStatus để lưu dưới dạng chuỗi và có giá trị mặc định là Pending
            builder.Property(o => o.OrderStatus)
                .HasConversion<string>()
                .HasDefaultValue(OrderStatus.Pending)
                .HasMaxLength(50)
                .IsRequired();

            // Cấu hình các thuộc tính theo dõi thời gian và người dùng, với CreatedAt có giá trị mặc định là thời điểm hiện tại
            builder.Property(o => o.CreatedBy).IsRequired();
            builder.Property(o => o.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(o => o.UpdatedBy).IsRequired(false);
            builder.Property(o => o.UpdatedAt).IsRequired(false);
            builder.Property(o => o.DeletedBy).IsRequired(false);
            builder.Property(o => o.DeletedAt).IsRequired(false);

            // Cấu hình các chỉ mục để tối ưu hóa hiệu suất truy vấn
            builder.HasIndex(o => o.CustomerId);
            builder.HasIndex(o => o.TableId);
            builder.HasIndex(o => o.OrderStatus);
            builder.HasIndex(o => o.CreatedAt);
            builder.HasIndex(o => new { o.OrderStatus, o.CreatedAt });

            // Cấu hình filter
            builder.HasQueryFilter(o => o.DeletedAt == null);

            // Thiết lập quan hệ một-nhiều giữa Order và OrderItem,
            // với xóa cascade để đảm bảo rằng khi một Order bị xóa,
            // tất cả các OrderItem liên quan cũng sẽ bị xóa
            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Table)
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.SetNull);

            // Thêm các ràng buộc kiểm tra để đảm bảo tính nhất quán của dữ liệu
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("ck_orders_tax_non_negative", "tax >= 0");
                table.HasCheckConstraint("ck_orders_discount_non_negative", "discount >= 0");
                table.HasCheckConstraint("ck_orders_sub_total_non_negative", "sub_total >= 0");
                table.HasCheckConstraint("ck_orders_discount_less_than_sub_total", "discount <= sub_total");
                table.HasCheckConstraint("ck_orders_total_amount_non_negative", "total_amount >= 0");
                table.HasCheckConstraint("ck_orders_total_amount_consistent", "total_amount = sub_total + tax - discount");
            });
        }
    }
}
