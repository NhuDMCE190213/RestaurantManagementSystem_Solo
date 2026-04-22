# Development Workflow

## 1. Nguyên tắc chung

- Làm việc theo task nhỏ, rõ phạm vi
- Mỗi task có issue tương ứng
- Ưu tiên code dễ đọc hơn code quá tối ưu sớm

## 2. Quy trình thực hiện một task

1. Tạo issue mô tả mục tiêu và tiêu chí hoàn thành.
2. Tạo branch từ dev theo chuẩn feature/* hoặc fix/*.
3. Thiết kế nhanh dữ liệu, API và tác động UI.
4. Code theo đúng convention BE/FE.
5. Tự test chức năng chính và tình huống lỗi.
6. Commit theo semantic prefix.
7. Mở PR và tự review bằng checklist.
8. Merge vào dev khi đạt yêu cầu.

## 3. Definition of Done

- Build pass
- Không có lỗi runtime ở luồng chính
- Không vi phạm convention đã thống nhất
- Đã cập nhật tài liệu nếu thay đổi hành vi

## 4. Thứ tự ưu tiên phát triển

1. Auth + phân quyền
2. Danh mục + món ăn
3. Bàn + đơn hàng + hóa đơn
4. Báo cáo cơ bản
5. Tối ưu hiệu năng và trải nghiệm

## 5. Quản lý chất lượng

- Mỗi tuần rà lại technical debt một lần
- Theo dõi lỗi bằng GitHub Issues
- Không để nhánh feature kéo dài quá lâu
