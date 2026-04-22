# Git Workflow (Solo Developer)

## 1. Nhánh sử dụng

- main: bản ổn định để release
- dev: nhánh phát triển chính
- feature/<ten-tinh-nang>: làm tính năng mới
- fix/<ten-loi>: sửa lỗi
- hotfix/<ten-loi-gap>: sửa lỗi khẩn cấp trên main

## 2. Quy trình làm việc

1. Tạo issue mô tả task.
2. Tạo branch mới từ dev.
3. Code và commit nhỏ, rõ ý nghĩa.
4. Rebase hoặc merge dev vào branch nếu lệch xa.
5. Mở Pull Request về dev.
6. Tự review theo checklist.
7. Merge vào dev khi đạt yêu cầu.
8. Khi đủ ổn định, merge dev vào main để release.

## 3. Commit message convention

- feat: thêm tính năng
- fix: sửa lỗi
- refactor: cải tiến code không đổi hành vi
- docs: cập nhật tài liệu
- test: thêm hoặc sửa test
- chore: việc cấu hình, hạ tầng, công cụ

Ví dụ:

```text
feat(auth): add login with JWT
fix(order): correct total amount calculation
docs(readme): update setup steps
```

## 4. Pull Request checklist tự review

- Đã chạy build thành công
- Đã chạy test liên quan
- Không còn TODO không cần thiết
- Không hardcode secret hoặc thông tin nhạy cảm
- Tên biến, hàm, class đúng convention
- Không đưa business logic vào sai layer
- Có cập nhật tài liệu nếu thay đổi hành vi

## 5. Quy tắc merge

- Không commit trực tiếp lên main
- Hạn chế PR quá lớn; mỗi PR nên tập trung một mục tiêu
- Khi conflict, xử lý xong phải chạy lại build và test

## 6. Gợi ý release

- Dùng tag phiên bản: v0.1.0, v0.2.0, v1.0.0
- Tạo release note ngắn gồm: tính năng mới, lỗi đã sửa, lưu ý deploy
