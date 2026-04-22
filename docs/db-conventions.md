# Database Conventions (PostgreSQL)

## 1. Mục tiêu

Thống nhất quy ước database để giảm lỗi mapping, dễ đọc schema, và thuận tiện bảo trì.

## 2. Quy ước đặt tên

- Tên bảng: snake_case, số nhiều, ví dụ: users, menu_items, order_items
- Tên cột: snake_case, ví dụ: created_at, table_id, unit_price
- Tên khóa chính: id
- Tên khóa ngoại: <bang_tham_chieu_singular>_id, ví dụ: user_id, order_id
- Tên index: idx_<table>_<column>, ví dụ: idx_orders_created_at
- Tên unique constraint: uq_<table>_<column>, ví dụ: uq_users_email

## 3. Chuẩn kiểu dữ liệu

- id: uuid (khuyến nghị) hoặc bigint
- Chuỗi ngắn: varchar(n)
- Chuỗi dài: text
- Tiền tệ: numeric(18,2)
- Số lượng: integer
- Trạng thái: smallint hoặc varchar ngắn theo enum mapping
- Thời gian: timestamptz
- Cờ logic: boolean

## 4. Cột audit bắt buộc

Mỗi bảng nghiệp vụ cần có:

- created_at timestamptz not null
- created_by varchar(100) null
- updated_at timestamptz null
- updated_by varchar(100) null

## 5. Soft delete

Nếu dùng xóa mềm, chuẩn như sau:

- is_deleted boolean not null default false
- deleted_at timestamptz null
- deleted_by varchar(100) null

Không xóa cứng dữ liệu nghiệp vụ nếu chưa có yêu cầu đặc biệt.

## 6. Quan hệ và ràng buộc

- Bắt buộc khai báo foreign key rõ ràng
- Chỉ dùng cascade delete khi chắc chắn phù hợp nghiệp vụ
- Không để dữ liệu mồ côi (orphan records)

## 7. Index conventions

Bắt buộc xem xét index cho:

- Các cột foreign key
- Các cột filter thường dùng: status, created_at
- Các cột tìm kiếm theo unique: email, code

Không lạm dụng index cho cột ít truy vấn ghi/đọc.

## 8. Migration conventions

- Mỗi thay đổi schema phải đi qua migration
- Tên migration theo mẫu: yyyyMMdd_HHmm_<noi_dung>, ví dụ: 20260422_1030_add_orders_table
- Không sửa migration đã áp dụng ở môi trường chia sẻ
- Nếu cần thay đổi, tạo migration mới

## 9. Seed data

Seed các dữ liệu nền tảng:

- roles: Admin, Staff
- categories mặc định (nếu có)

Seed phải idempotent để chạy lại không lỗi.

## 10. Transaction conventions

- Dùng transaction cho luồng ghi nhiều bảng
- Đảm bảo rollback đầy đủ khi lỗi
- Không để transaction mở quá lâu

## 11. Bảo mật dữ liệu

- Không lưu plain text password
- Không lưu secret/token nhạy cảm dưới dạng không mã hóa
- Hạn chế quyền DB user theo nguyên tắc ít quyền nhất
