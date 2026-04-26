# Entity Workflow

## 1. Mục tiêu

Tài liệu này ghi lại quy trình chuẩn khi thêm, sửa, hoặc xóa entity trong backend để tránh lệch giữa Domain, Infrastructure, và Database.

## 2. Thêm entity mới

### Bước 1: Tạo entity trong Domain

- Tạo file entity trong `RestaurantManagement.Domain/Entities`
- Entity chỉ chứa dữ liệu và behavior cốt lõi của domain
- Không đưa mapping, EF Core, hoặc logic hạ tầng vào đây

### Bước 2: Thêm DbSet vào AppDbContext

- Mở `RestaurantManagement.Infrastructure/AppDbContext.cs`
- Thêm `DbSet<TEntity>` nếu entity cần truy cập trực tiếp
- Giữ `AppDbContext` gọn, chỉ chứa DbSet và gọi configuration

### Bước 3: Tạo configuration riêng

- Tạo file trong `RestaurantManagement.Infrastructure/Configurations`
- Mỗi entity nên có một file configuration riêng
- Dùng `IEntityTypeConfiguration<TEntity>` để map bảng, cột, khóa, index, quan hệ

### Bước 4: Áp dụng quy chuẩn DB

- Đặt tên bảng theo convention của dự án
- Kiểm tra audit columns nếu bảng nghiệp vụ cần theo dõi lịch sử
- Xem có cần soft delete không
- Thêm index cho các cột hay filter hoặc search

### Bước 5: Tạo migration

- Tạo migration mới sau khi entity và configuration đã xong
- Không sửa migration cũ nếu đã áp dụng ở môi trường chung
- Kiểm tra lại tên bảng, cột, kiểu dữ liệu, và ràng buộc

### Bước 6: Update database

- Chạy update database sau khi migration hợp lệ
- Kiểm tra lại bảng mới trong database
- Xác nhận schema khớp với entity và configuration

### Bước 7: Thêm test hoặc seed nếu cần

- Thêm seed data nếu entity là dữ liệu nền
- Thêm unit test hoặc integration test nếu entity ảnh hưởng đến luồng nghiệp vụ

## 3. Sửa entity hiện có

- Sửa entity trong Domain trước
- Đồng bộ lại file configuration tương ứng
- Nếu schema thay đổi, tạo migration mới
- Cập nhật test liên quan nếu có
- Chạy lại build và kiểm tra warning

## 4. Xóa entity

- Xóa hoặc ngừng dùng entity trong Domain
- Xóa configuration tương ứng trong Infrastructure
- Xóa `DbSet<TEntity>` nếu có
- Tạo migration để drop bảng hoặc cột liên quan
- Dọn code sử dụng entity đó ở Application, API, và test

## 5. Checklist nhanh

- [ ] Entity đã tạo trong Domain
- [ ] `DbSet` đã thêm vào `AppDbContext` nếu cần
- [ ] Configuration đã tách riêng trong Infrastructure
- [ ] Convention DB đã được áp dụng
- [ ] Migration mới đã được tạo
- [ ] Database đã update
- [ ] Test liên quan đã chạy lại

## 6. Ghi nhớ

- Domain không phụ thuộc EF Core
- Infrastructure giữ toàn bộ mapping và cấu hình database
- Mỗi thay đổi schema nên đi qua migration
- Không sửa migration đã áp dụng nếu dự án đã chia sẻ cho nhiều người