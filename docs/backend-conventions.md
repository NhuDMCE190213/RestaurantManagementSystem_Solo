# Backend Conventions (ASP.NET Core 9 + Clean Architecture)

## 1. Mục tiêu

Tài liệu này quy định chuẩn code backend để đảm bảo code dễ đọc, dễ mở rộng, dễ test.

## 2. Cấu trúc thư mục

```text
backend/
  src/
    RestaurantManagement.Domain/
    RestaurantManagement.Application/
    RestaurantManagement.Infrastructure/
    RestaurantManagement.API/
  tests/
    RestaurantManagement.UnitTests/
    RestaurantManagement.IntegrationTests/
```

## 3. Quy ước đặt tên

- Class, Interface, Method, Property: PascalCase
- Biến cục bộ, tham số: camelCase
- Interface bắt đầu bằng I, ví dụ: IUserRepository
- Hằng số: PascalCase cho const; UPPER_CASE chỉ dùng khi bắt buộc theo chuẩn ngoài
- Tên bất đồng bộ kết thúc bằng Async, ví dụ: GetByIdAsync

## 4. Quy ước theo layer

- Domain:
  - Chỉ chứa Entity, Value Object, Enum, Domain Event
  - Không tham chiếu EF Core, HTTP, Database
- Application:
  - Chứa Use Case, DTO, Interface, Validator
  - Không chứa code truy cập DB trực tiếp
- Infrastructure:
  - Cài đặt Repository, DbContext, service ngoài
  - Không chứa business rule cốt lõi
- API:
  - Chỉ nhận request, gọi Application, trả response
  - Không viết business logic trong Controller

## 5. API conventions

- Route dùng số nhiều, ví dụ: /api/users, /api/orders
- Version API bằng đường dẫn, ví dụ: /api/v1/orders
- Controller tên theo tài nguyên, ví dụ: OrdersController
- Trạng thái HTTP:
  - 200: Thành công
  - 201: Tạo mới thành công
  - 400: Dữ liệu không hợp lệ
  - 401: Chưa xác thực
  - 403: Không có quyền
  - 404: Không tìm thấy
  - 409: Xung đột dữ liệu
  - 500: Lỗi hệ thống

## 6. Chuẩn response

Success:

```json
{
  "success": true,
  "message": "OK",
  "data": {}
}
```

Error:

```json
{
  "success": false,
  "errorCode": "VALIDATION_ERROR",
  "message": "Dữ liệu không hợp lệ",
  "details": []
}
```

## 7. Validation và lỗi

- Dùng FluentValidation ở Application layer
- Dùng middleware xử lý exception tập trung
- Không trả stack trace ra client
- Mã lỗi nhất quán theo danh sách error code

## 8. JWT và bảo mật

- Access token ngắn hạn, khuyến nghị 15-60 phút
- Refresh token lưu an toàn, có cơ chế revoke
- Secret key lưu qua biến môi trường
- Password hash bằng ASP.NET Identity PasswordHasher hoặc BCrypt
- Bật Authorize theo role cho endpoint nhạy cảm

## 9. Logging

- Dùng ILogger
- Log theo mức: Information, Warning, Error, Critical
- Không log thông tin nhạy cảm: mật khẩu, token, secret
- Mỗi request nên có CorrelationId để truy vết

## 10. Testing

- Unit test cho Application use case
- Integration test cho API quan trọng: auth, order, billing
- Tên test theo mẫu: MethodName_State_ExpectedResult

## 11. Định dạng và chất lượng code

- Bật nullable reference types
- Cảnh báo build không được bỏ qua
- Ưu tiên dependency injection thay vì new trực tiếp
- Mỗi class nên có một trách nhiệm chính
