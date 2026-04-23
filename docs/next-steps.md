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
- [ ] Cấu hình JWT trong API
- [ ] Cấu hình CORS theo môi trường local
- [ ] Tạo DbContext và đăng ký DI cho Infrastructure
- [ ] Tạo migration đầu tiên và update database
- [ ] Tạo endpoint health check (`/health`)
- [ ] Kiểm tra Swagger chạy được và gọi thử endpoint

## 4. Việc cần làm trong milestone 1 (P1)

- [ ] Thiết kế schema người dùng, role, quyền cơ bản
- [ ] Xây dựng luồng Auth: login, refresh token, logout
- [ ] Bảo vệ endpoint bằng `[Authorize]` và role policy
- [ ] Chuẩn hóa response lỗi/thành công theo convention
- [ ] Thêm global exception middleware
- [ ] Viết unit test cho use case Auth
- [ ] Viết integration test cho endpoint đăng nhập

## 5. Việc cần làm sau khi có Auth (P2)

- [ ] Module Category
- [ ] Module Menu Item
- [ ] Module Table Management
- [ ] Module Order
- [ ] Module Invoice/Billing
- [ ] Seed dữ liệu mẫu để test luồng end-to-end

## 6. Frontend song song

- [ ] Khởi tạo app TypeScript (React + Vite)
- [ ] Thiết lập router + layout khung
- [ ] Thiết lập API client và interceptor JWT
- [ ] Làm màn hình Login
- [ ] Làm màn hình Table Management
- [ ] Làm màn hình POS/Order
- [ ] Làm màn hình Checkout

## 7. Chất lượng và vận hành

- [ ] Bật lint/format cho backend và frontend
- [ ] Thiết lập CI GitHub Actions: restore + build + test
- [ ] Viết nhật ký phát triển theo ngày
- [ ] Cập nhật tài liệu khi có thay đổi kiến trúc/package

## 8. Definition of Done cho mỗi task

- [ ] Build pass
- [ ] Test liên quan pass
- [ ] Không thêm warning nghiêm trọng
- [ ] Đúng convention backend/frontend
- [ ] Có cập nhật docs nếu thay đổi hành vi

## 9. Gợi ý thứ tự commit

1. chore(setup): fix package versions and build
2. feat(core): add DbContext and infrastructure DI
3. feat(auth): implement JWT login and refresh flow
4. test(auth): add unit and integration tests
5. docs(next-steps): update progress checklist
