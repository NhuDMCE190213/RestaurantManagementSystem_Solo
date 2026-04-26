# Next Steps

## 1. Mục tiêu

Tài liệu này liệt kê các công việc cần làm tiếp theo theo thứ tự ưu tiên để triển khai dự án ổn định và tránh thiếu bước nền tảng.

## 2. Công việc đã hoàn thành

- [x] Tạo cấu trúc backend chuẩn: `RestaurantManagement.sln`, `src/`, `tests/`
- [x] Tạo đủ 4 project chính: API, Application, Domain, Infrastructure
- [x] Tạo đủ 2 project test: UnitTests, IntegrationTests
- [x] Thiết lập project reference theo Clean Architecture
- [x] Thiết lập package nền cho backend theo từng layer
- [x] Thiết lập package test cơ bản cho UnitTests và IntegrationTests
- [x] Tạo bộ tài liệu kỹ thuật trong `docs/` (architecture, conventions, workflow, packages, tooling, ux/ui)
- [x] Tạo file checklist tiến độ `docs/next-steps.md`

## 3. Việc cần làm ngay (P0)

- [x] Restore và build toàn bộ solution
- [x] Xử lý toàn bộ lỗi package/version conflict
- [x] Cấu hình JWT trong API
- [x] Cấu hình CORS theo môi trường local
- [x] Tạo DbContext và đăng ký DI cho Infrastructure
- [x] Tạo migration đầu tiên và update database
- [x] Tạo endpoint health check (`/health`)
- [x] Kiểm tra Swagger chạy được và gọi thử endpoint

## 4. Trục chính hệ thống

Trục chính của hệ thống là luồng bán hàng Order: tạo order -> thêm món -> tính tiền -> thanh toán -> lưu lịch sử.

Các milestone bên dưới ưu tiên trực tiếp cho trục này, không làm dàn trải.

## 5. Việc cần làm trong milestone 1 (P1) - Xây dựng Order core

- [ ] Thiết kế schema người dùng, role, quyền cơ bản (mức đủ dùng cho đăng nhập)
- [ ] Xây dựng Auth tối thiểu cho vận hành Order: login, refresh token, logout
- [ ] Thiết kế schema cốt lõi cho Order: order, order_item, table (hoặc dining_table)
- [ ] Xây dựng API tạo order và thêm/xóa/cập nhật order item
- [ ] Xây dựng logic tính subtotal, tax, discount, total cho order
- [ ] Xây dựng API đổi trạng thái order (open, paid, cancelled)
- [ ] Bảo vệ endpoint bằng Authorize và role policy cơ bản (Admin/Staff)
- [ ] Chuẩn hóa response lỗi/thành công theo convention
- [ ] Thêm global exception middleware
- [ ] Viết unit test cho use case Order core và Auth tối thiểu
- [ ] Viết integration test cho endpoint login và order flow cơ bản

## 6. Việc cần làm trong milestone 2 (P2) - Hoàn thiện nghiệp vụ liên quan

- [ ] Module Category
- [ ] Module Menu Item
- [ ] Module Table Management đầy đủ
- [ ] Module Invoice/Billing chi tiết
- [ ] Seed dữ liệu mẫu đủ để test end-to-end cho luồng Order

## 7. Frontend song song (ưu tiên theo Order)

- [ ] Khởi tạo app TypeScript (React + Vite)
- [ ] Thiết lập router + layout khung
- [ ] Thiết lập API client và interceptor JWT
- [ ] Làm màn hình Login
- [ ] Làm màn hình POS/Order (ưu tiên cao nhất)
- [ ] Làm màn hình Checkout
- [ ] Làm màn hình Table Management

## 8. Chất lượng và vận hành

- [ ] Bật lint/format cho backend và frontend
- [ ] Thiết lập CI GitHub Actions: restore + build + test
- [ ] Viết nhật ký phát triển theo ngày
- [ ] Cập nhật tài liệu khi có thay đổi kiến trúc/package

## 9. Definition of Done cho mỗi task

- [ ] Build pass
- [ ] Test liên quan pass
- [ ] Không thêm warning nghiêm trọng
- [ ] Đúng convention backend/frontend
- [ ] Có cập nhật docs nếu thay đổi hành vi
- [ ] Không làm gãy luồng Order end-to-end

## 10. Gợi ý thứ tự commit (tập trung Order)

1. feat(auth): auth tối thiểu cho hệ thống (login, refresh, logout)
2. feat(order-schema): thêm schema order core và migration
3. feat(order-api): API tạo và cập nhật order/order item
4. feat(order-calc): logic tính tiền order (subtotal, tax, discount, total)
5. test(order): unit và integration test cho auth + order flow cơ bản
6. docs(next-steps): cập nhật tiến độ theo trục Order
