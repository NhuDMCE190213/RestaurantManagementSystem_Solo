# UI Style Guide (MVP)

## 1. Mục tiêu

Thiết lập style guide tối thiểu để UI nhất quán, dễ mở rộng, và giảm tranh cãi khi triển khai.

## 2. Thiết kế thị giác

Hướng thiết kế:
- Sạch, hiện đại, ưu tiên tốc độ thao tác cho staff
- Độ tương phản cao cho môi trường ánh sáng mạnh trong nhà hàng

## 3. Màu sắc đề xuất

- Primary: #0D6EFD
- Secondary: #2B2F36
- Background: #F7F9FC
- Surface: #FFFFFF
- Success: #198754
- Warning: #F59F00
- Error: #DC3545
- Info: #0DCaf0

Quy ước trạng thái bàn:
- Trống: xanh lá
- Đang phục vụ: cam
- Chờ thanh toán: xanh dương
- Khóa/không phục vụ: xám

## 4. Typography

- Font chính: Inter hoặc Nunito Sans
- Heading: semibold/bold
- Body: regular/medium
- Cỡ chữ tối thiểu cho nội dung chính: 14px

Scale đề xuất:
- H1: 32px
- H2: 24px
- H3: 20px
- Body: 14px-16px
- Caption: 12px

## 5. Spacing và layout

- Grid: 12 cột (desktop)
- Khoảng cách theo bậc: 4/8/12/16/24/32
- Khoảng cách tối thiểu giữa các vùng thao tác: 12px
- Border radius: 8px (mặc định), 12px (card lớn)

## 6. Thành phần tương tác

Button:
- Primary: hành động chính
- Secondary: hành động phụ
- Danger: hành động hủy/xóa

Input:
- Luôn có label
- Placeholder chỉ mang tính gợi ý, không thay thế label
- Báo lỗi ngay dưới input

Modal:
- Có tiêu đề rõ nghĩa
- Có nút đóng và nút hành động chính
- Hỗ trợ phím Escape

## 7. Motion

- Thời lượng transition: 150-250ms
- Dùng easing nhẹ cho hover/focus
- Tránh animation dài gây chậm thao tác quầy

## 8. Responsive

Breakpoints đề xuất:
- Mobile: < 768px
- Tablet: 768px - 1023px
- Desktop: >= 1024px

Nguyên tắc:
- POS ưu tiên desktop/tablet
- Màn hình quản trị vẫn dùng được trên laptop phổ thông 1366px

## 9. Ngôn ngữ hiển thị

- Ưu tiên tiếng Việt rõ ràng, ngắn gọn
- Tránh thuật ngữ nội bộ gây khó hiểu cho nhân viên mới
- Thông báo lỗi phải nêu cách xử lý nếu có thể

## 10. Quy tắc nhất quán

- Một hành động chỉ dùng một kiểu tên gọi trên toàn app
- Một trạng thái chỉ dùng một màu
- Một component dùng một cách tương tác nhất quán ở mọi màn hình
