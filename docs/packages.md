# Package Matrix

## 1. Mục tiêu

Tài liệu này liệt kê các package nên cài theo từng project để giữ đúng Clean Architecture và tránh cài nhầm layer.

## 2. Quy ước version

- Ưu tiên version tương thích với .NET 9
- Package của Microsoft nên ưu tiên nhánh 9.x khi có
- Package bên thứ ba chọn bản stable mới nhất còn hỗ trợ net9

## 3. Bộ package cốt lõi

Đây là bộ nên có ngay từ đầu để project chạy được đúng kiến trúc.

- API: JWT + Swagger
- Infrastructure: EF Core + PostgreSQL
- Application: Validation + mapping
- Tests: xUnit + assertions

## 4. RestaurantManagement.API

### Bắt buộc

- Microsoft.AspNetCore.Authentication.JwtBearer (9.0.15): xác thực và phân quyền JWT cho API
- Swashbuckle.AspNetCore (9.0.6): tạo Swagger UI và OpenAPI document
- Microsoft.AspNetCore.OpenApi (9.0.13): hỗ trợ metadata OpenAPI chi tiết hơn

### Tùy chọn

- Microsoft.AspNetCore.Mvc.NewtonsoftJson (chưa cài): thay thế serializer mặc định bằng Newtonsoft cho các trường hợp cần tùy biến JSON sâu hơn

Mục đích:
- Xác thực JWT
- Tài liệu API với Swagger/OpenAPI
- Tùy biến format JSON nếu cần

## 5. RestaurantManagement.Infrastructure

### Bắt buộc

- Microsoft.EntityFrameworkCore (9.0.15): lõi ORM của EF Core để làm việc với entity và DbContext
- Microsoft.EntityFrameworkCore.Relational (9.0.15): các tính năng chung cho database quan hệ
- Microsoft.EntityFrameworkCore.Design (9.0.15): hỗ trợ design-time cho migration và scaffold
- Microsoft.EntityFrameworkCore.Tools (9.0.15): hỗ trợ lệnh và thao tác migration trong quá trình phát triển
- Npgsql.EntityFrameworkCore.PostgreSQL (9.0.4): provider EF Core cho PostgreSQL
- Npgsql (9.0.5): driver PostgreSQL gốc để kết nối database

### Tùy chọn

- Microsoft.Extensions.Configuration.Abstractions (9.0.15): đọc cấu hình theo abstraction khi cần truy cập config ở layer này
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.0): khai báo extension method đăng ký DI mà không phụ thuộc implementation cụ thể

Mục đích:
- Làm việc với Entity Framework Core
- Kết nối PostgreSQL
- Migration và thiết kế database
- Đăng ký DI, đọc config khi cần

## 6. RestaurantManagement.Application

### Bắt buộc

- FluentValidation (9.5.4): validate input, command, query, DTO
- FluentValidation.DependencyInjectionExtensions (9.5.4): đăng ký validator vào DI container tự động

### Tùy chọn

- MediatR (14.1.0): tách request/handler rõ ràng, giảm phụ thuộc giữa các use case
- AutoMapper (16.1.1): map giữa entity, DTO, view model
- Mapster (chưa cài): lựa chọn thay thế AutoMapper
- Mapster.DependencyInjection (chưa cài): đăng ký Mapster vào DI container
- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.0): tạo extension cấu hình Application layer mà không kéo phụ thuộc thừa

Mục đích:
- Validation input
- Mapping DTO và model
- Tổ chức use case rõ hơn nếu dùng CQRS

## 7. RestaurantManagement.Domain

- Thường không cần package ngoài

Mục đích:
- Giữ domain sạch, ít phụ thuộc nhất có thể

## 8. RestaurantManagement.UnitTests

### Bắt buộc

- Microsoft.NET.Test.Sdk (17.12.0): chạy test project trong .NET test host
- xunit (2.9.2): framework test chính
- xunit.runner.visualstudio (2.8.2): tích hợp chạy test trong Visual Studio/Test Explorer
- FluentAssertions (8.9.0): viết assert dễ đọc và rõ nghĩa hơn
- coverlet.collector (6.0.2): thu thập code coverage khi chạy test

Mục đích:
- Test logic ở Application/Domain

### Tùy chọn

- Moq (4.20.72): tạo dependency giả để test logic isolated
- NSubstitute (chưa cài): lựa chọn thay thế Moq

## 9. RestaurantManagement.IntegrationTests

### Bắt buộc

- Microsoft.NET.Test.Sdk (17.12.0): chạy test project trong .NET test host
- xunit (2.9.2): framework test chính
- xunit.runner.visualstudio (2.8.2): tích hợp chạy test trong Visual Studio/Test Explorer
- FluentAssertions (8.9.0): assert dễ đọc và dễ bảo trì
- coverlet.collector (6.0.2): thu thập code coverage khi chạy test
- Microsoft.AspNetCore.Mvc.Testing (9.0.15): host API cho integration test qua WebApplicationFactory

Mục đích:
- Test API end-to-end hoặc gần end-to-end

### Tùy chọn

- Moq (4.20.72): giả lập một số service ngoài khi test luồng tích hợp

## 10. Ghi chú migration

- Nếu dùng EF Core migration theo `dotnet ef` (CLI), cài thêm `dotnet-ef` dưới dạng global tool:

```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
dotnet ef --version
```

- Không cần cài `dotnet-ef` như package trong project.
- Project chứa DbContext/migration: `RestaurantManagement.Infrastructure`.
- Startup project: `RestaurantManagement.API`.

Lệnh thông dụng với `dotnet ef` (chạy tại thư mục `backend`):

```bash
dotnet ef migrations add InitialCreate --project src/RestaurantManagement.Infrastructure --startup-project src/RestaurantManagement.API
dotnet ef database update --project src/RestaurantManagement.Infrastructure --startup-project src/RestaurantManagement.API
dotnet ef migrations remove --project src/RestaurantManagement.Infrastructure --startup-project src/RestaurantManagement.API
```

Lệnh tương đương trong Package Manager Console (PMC):

```powershell
Add-Migration InitialCreate -Project RestaurantManagement.Infrastructure -StartupProject RestaurantManagement.API
Update-Database -Project RestaurantManagement.Infrastructure -StartupProject RestaurantManagement.API
Remove-Migration -Project RestaurantManagement.Infrastructure -StartupProject RestaurantManagement.API
```

## 11. Ghi chú thực tế

- Danh sách trên là bộ khởi đầu hợp lý cho dự án của bạn
- Khi phát sinh tính năng như email, caching, upload file, logging nâng cao thì mới thêm package tương ứng
