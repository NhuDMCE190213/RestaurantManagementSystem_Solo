# Architecture Overview

## 1. Kiến trúc áp dụng

Dự án sử dụng Clean Architecture với 4 layer chính:

- Domain
- Application
- Infrastructure
- API

Dependency chỉ đi từ ngoài vào trong.

## 2. Trách nhiệm từng layer

- Domain:
  - Chứa Entity, Value Object, Enum, Domain Rule
  - Không phụ thuộc framework hay database
- Application:
  - Chứa Use Case, DTO, Interface, Validator
  - Điều phối nghiệp vụ
- Infrastructure:
  - Triển khai truy cập dữ liệu, dịch vụ ngoài
  - Cài đặt interface từ Application
- API:
  - Expose endpoint HTTP
  - Mapping request/response, auth, middleware

## 3. Luồng xử lý cơ bản

1. Client gửi request đến API.
2. Controller gọi Use Case trong Application.
3. Use Case dùng interface repository/service.
4. Infrastructure triển khai interface để lấy/ghi dữ liệu.
5. API trả response chuẩn hóa.

## 4. Quy tắc phụ thuộc

- Domain không tham chiếu layer nào khác.
- Application chỉ tham chiếu Domain.
- Infrastructure tham chiếu Application và Domain.
- API tham chiếu Application và Infrastructure.

## 5. Bảo mật

- Xác thực qua JWT Bearer Token
- Phân quyền theo role: Admin, Staff
- Secret và connection string quản lý qua môi trường
