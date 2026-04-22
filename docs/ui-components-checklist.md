# UI Components Checklist

## 1. Mục tiêu

Danh sách component cần có để xây UI nhất quán và tránh thiếu thành phần khi triển khai theo module.

## 2. Foundation

- Color tokens: primary, neutral, success, warning, error
- Typography scale: h1-h6, body, caption
- Spacing scale: 4, 8, 12, 16, 24, 32
- Radius và shadow chuẩn
- Icon set thống nhất

## 3. Core components

- Button (primary, secondary, danger, ghost)
- Input text
- Password input có toggle
- Textarea
- Select
- Checkbox
- Radio
- Switch
- Badge
- Tag/Chip
- Tooltip
- Divider

## 4. Feedback components

- Toast notification
- Alert inline
- Confirm dialog
- Loading spinner
- Skeleton loader
- Empty state block
- Error state block

## 5. Data display components

- Table (sort, filter, pagination)
- Card
- List item
- Statistic widget
- Status pill (trạng thái bàn, trạng thái đơn)

## 6. Navigation components

- Sidebar
- Topbar
- Breadcrumb
- Tabs
- Pagination
- Search bar

## 7. Domain-specific components (Restaurant)

- Table card (mã bàn, trạng thái, thời gian sử dụng)
- Menu item card (tên món, giá, trạng thái)
- Order item row (món, số lượng, ghi chú)
- Bill summary panel (tạm tính, thuế, giảm giá, tổng)
- Payment method selector

## 8. Screen checklist

### 8.1 Login

- Email input
- Password input
- Submit button
- Error alert

### 8.2 Table Management

- Bộ lọc khu vực
- Grid table card
- Legend trạng thái bàn

### 8.3 POS/Order

- Search món
- Danh mục món
- Danh sách món
- Giỏ đơn bên phải
- Nút lưu tạm/gửi bếp/thanh toán

### 8.4 Checkout

- Bill detail table
- Mã giảm giá (nếu có)
- Chọn phương thức thanh toán
- Nút xác nhận thanh toán

### 8.5 Menu Management

- Data table món
- Form tạo/sửa món
- Modal xác nhận xóa

## 9. Accessibility tối thiểu

- Focus visible cho keyboard
- Contrast màu đủ đọc
- Label gắn với input
- Enter/Space thao tác được nút chính

## 10. Definition of Done cho mỗi component

- Có trạng thái mặc định, hover, active, disabled
- Có loading state nếu cần
- Có ví dụ sử dụng trong ít nhất 1 màn hình
- Không phát sinh warning TypeScript/lint
