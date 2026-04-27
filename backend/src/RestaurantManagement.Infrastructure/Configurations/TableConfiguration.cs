using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Infrastructure.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            // Cấu hình tên bảng
            builder.ToTable("Tables");

            // Cấu hình khóa chính và đảm bảo rằng TableId không được tự động sinh ra
            builder.HasKey(t => t.TableId);
            builder.Property(t => t.TableId).ValueGeneratedNever();

            // Cấu hình các thuộc tính
            builder.Property(t => t.TableNumber)
                .IsRequired();
            builder.Property(t => t.Capacity)
                .HasDefaultValue(1) // Giá trị mặc định là 1 nếu không được cung cấp
                .IsRequired();

            // Cấu hình TableStatus để lưu dưới dạng chuỗi và có giá trị mặc định là Available
            builder.Property(t => t.TableStatus)
                .HasConversion<string>()
                .HasDefaultValue(TableStatus.Available)
                .IsRequired()
                .HasMaxLength(50);

            // Cấu hình thuộc tính BaseEntity
            builder.Property(o => o.CreatedBy).IsRequired();
            builder.Property(o => o.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
            builder.Property(o => o.UpdatedBy).IsRequired(false);
            builder.Property(o => o.UpdatedAt).IsRequired(false);
            builder.Property(o => o.DeletedBy).IsRequired(false);
            builder.Property(o => o.DeletedAt).IsRequired(false);

            // Cấu hình index để tối ưu hóa hiệu suất truy vấn
            builder.HasIndex(t => t.TableNumber).IsUnique();
            builder.HasIndex(t => t.TableStatus);
            builder.HasIndex(t => t.Capacity);
            builder.HasIndex(t => t.CreatedAt);
            builder.HasIndex(t => new { t.TableStatus, t.Capacity });
            builder.HasIndex(t => new { t.TableNumber, t.CreatedAt });

            // Cấu hình filter
            builder.HasQueryFilter(t => t.DeletedAt == null);

            // Cấu hình các ràng buộc đảm bảo tính nhất của dữ liệu
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("CK_Table_Capacity", "Capacity > 0");
            });

        }
    }
}
