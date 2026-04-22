# Tooling and External Software

## 1. Mục tiêu

Liệt kê các công cụ ngoài được dùng trong dự án để thống nhất môi trường làm việc.

## 2. Runtime và SDK

- .NET SDK 9.x
- Node.js LTS (khuyến nghị 20.x trở lên)
- PostgreSQL 15+ (khuyến nghị)

## 3. Công cụ phát triển

- Visual Studio Code (front end)
- Visual Studio (Backend)
- Git
- Docker Desktop (tùy chọn cho local services)

## 4. Công cụ database

- pgAdmin hoặc DBeaver
- Mục đích: quản lý schema, kiểm tra dữ liệu, chạy query

## 5. Công cụ API

- Postman hoặc Bruno
- Mục đích: test API, quản lý collection endpoint

## 6. Sơ đồ và tài liệu

- Mermaid
- Mục đích: vẽ sơ đồ kiến trúc, sequence, ERD trong Markdown
- Quy ước: lưu block Mermaid trực tiếp trong file docs để theo dõi bằng Git

## 7. Chất lượng code

Backend:

- dotnet format
- xUnit + FluentAssertions

Frontend:

- ESLint
- Prettier
- TypeScript strict mode

## 8. CI/CD

- GitHub Actions
- Job cơ bản: restore, build, test

## 9. Quản lý công việc

- GitHub Issues
- GitHub Projects
- Pull Request với checklist tự review

## 10. Ghi chú phiên bản

Khi đổi major version của SDK hoặc tool chính, cần cập nhật file này và README để tránh lệch môi trường.
