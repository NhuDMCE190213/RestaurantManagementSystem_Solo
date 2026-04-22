# UX Flow - Restaurant Management System

## 1. Mục tiêu

Định nghĩa luồng người dùng chính cho MVP để đảm bảo trải nghiệm mạch lạc trước khi vào code UI chi tiết.

## 2. Vai trò người dùng

- Admin: quản lý hệ thống, danh mục, món ăn, người dùng, báo cáo
- Staff: vận hành tại quầy, tạo đơn, xử lý thanh toán

## 3. User flow ưu tiên cao

### 3.1. Đăng nhập

1. Người dùng mở trang đăng nhập
2. Nhập email và mật khẩu
3. Hệ thống xác thực
4. Điều hướng theo vai trò (Admin/Staff)

Điểm UX cần có:
- Báo lỗi rõ ràng khi sai tài khoản
- Có trạng thái loading khi submit
- Chặn submit nhiều lần liên tiếp

### 3.2. Quản lý bàn

1. Staff vào màn hình sơ đồ bàn
2. Xem trạng thái bàn: trống, đang phục vụ, chờ thanh toán
3. Chọn bàn để tạo hoặc tiếp tục đơn

Điểm UX cần có:
- Màu trạng thái nhất quán
- Cập nhật trạng thái theo thời gian thực hoặc refresh nhanh
- Dễ thao tác trên màn hình cảm ứng

### 3.3. Tạo order/POS

1. Chọn bàn
2. Tìm món theo danh mục hoặc ô tìm kiếm
3. Thêm món vào giỏ đơn
4. Điều chỉnh số lượng, ghi chú món
5. Xác nhận lưu đơn tạm hoặc gửi bếp

Điểm UX cần có:
- Tìm món nhanh
- Không mất dữ liệu khi chuyển màn hình
- Tổng tiền cập nhật tức thời

### 3.4. Thanh toán hóa đơn

1. Mở hóa đơn từ bàn đang phục vụ
2. Kiểm tra món, số lượng, tổng tiền
3. Chọn phương thức thanh toán
4. Xác nhận thanh toán
5. In hóa đơn hoặc xuất bản điện tử

Điểm UX cần có:
- Có bước xác nhận cuối
- Hiển thị rõ tiền trước thuế, thuế, giảm giá, tổng cộng
- Sau thanh toán tự đổi trạng thái bàn về trống

### 3.5. Quản lý menu (Admin)

1. Admin vào module menu
2. Tạo/sửa/xóa danh mục
3. Tạo/sửa/xóa món ăn
4. Bật/tắt trạng thái bán

Điểm UX cần có:
- Form đơn giản, dễ nhập nhanh
- Cảnh báo khi xóa dữ liệu quan trọng
- Có lọc theo trạng thái và danh mục

## 4. Danh sách màn hình MVP

- Login
- Dashboard
- Table Management
- POS/Order Management
- Invoice/Checkout
- Menu Management
- Category Management
- User Management

## 5. Trạng thái UX bắt buộc cho mọi màn hình

- Loading state
- Empty state
- Error state
- Success feedback (toast/alert nhẹ)
- Unauthorized state

## 6. Điều hướng đề xuất

- Sidebar cho desktop
- Bottom navigation hoặc compact menu cho tablet
- Breadcrumb ở màn hình quản trị nhiều cấp

## 7. KPI UX giai đoạn đầu

- Tạo đơn hoàn tất trong dưới 60 giây với staff đã quen
- Thanh toán hoàn tất trong dưới 30 giây
- Tỷ lệ lỗi thao tác (nhầm món/nhầm bàn) giảm theo từng sprint
