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

- Microsoft.AspNetCore.Authentication.JwtBearer: xác thực và phân quyền JWT cho API
- Swashbuckle.AspNetCore: tạo Swagger UI và OpenAPI document
- Microsoft.AspNetCore.OpenApi nếu muốn cấu hình OpenAPI rõ hơn: hỗ trợ metadata OpenAPI chi tiết hơn

### Tùy chọn

- Microsoft.AspNetCore.Mvc.NewtonsoftJson nếu dùng Newtonsoft JSON: thay thế serializer mặc định bằng Newtonsoft cho các trường hợp cần tùy biến JSON sâu hơn

Mục đích:
- Xác thực JWT
- Tài liệu API với Swagger/OpenAPI
- Tùy biến format JSON nếu cần

## 5. RestaurantManagement.Infrastructure

### Bắt buộc

- Microsoft.EntityFrameworkCore: lõi ORM của EF Core để làm việc với entity và DbContext
- Microsoft.EntityFrameworkCore.Relational: các tính năng chung cho database quan hệ
- Microsoft.EntityFrameworkCore.Design: hỗ trợ design-time cho migration và scaffold
- Microsoft.EntityFrameworkCore.Tools: hỗ trợ lệnh và thao tác migration trong quá trình phát triển
- Npgsql.EntityFrameworkCore.PostgreSQL: provider EF Core cho PostgreSQL
- Npgsql: driver PostgreSQL gốc để kết nối database

### Tùy chọn

- Microsoft.Extensions.Configuration.Abstractions: đọc cấu hình theo abstraction khi cần truy cập config ở layer này
- Microsoft.Extensions.DependencyInjection.Abstractions: khai báo extension method đăng ký DI mà không phụ thuộc implementation cụ thể

Mục đích:
- Làm việc với Entity Framework Core
- Kết nối PostgreSQL
- Migration và thiết kế database
- Đăng ký DI, đọc config khi cần

## 6. RestaurantManagement.Application

### Bắt buộc

- FluentValidation: validate input, command, query, DTO
- FluentValidation.DependencyInjectionExtensions: đăng ký validator vào DI container tự động

### Tùy chọn

- MediatR nếu muốn dùng CQRS / mediator pattern: tách request/handler rõ ràng, giảm phụ thuộc giữa các use case
- Mapster hoặc AutoMapper: map giữa entity, DTO, view model
- Mapster.DependencyInjection nếu chọn Mapster: đăng ký Mapster vào DI container
- Microsoft.Extensions.DependencyInjection.Abstractions nếu cần extension method đăng ký service: tạo extension cấu hình Application layer mà không kéo phụ thuộc thừa

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

- Microsoft.NET.Test.Sdk: chạy test project trong .NET test host
- xunit: framework test chính
- xunit.runner.visualstudio: tích hợp chạy test trong Visual Studio/Test Explorer
- FluentAssertions: viết assert dễ đọc và rõ nghĩa hơn

Mục đích:
- Test logic ở Application/Domain

### Tùy chọn

- Moq hoặc NSubstitute nếu cần mock: tạo dependency giả để test logic isolated

## 9. RestaurantManagement.IntegrationTests

### Bắt buộc

- Microsoft.NET.Test.Sdk: chạy test project trong .NET test host
- xunit: framework test chính
- xunit.runner.visualstudio: tích hợp chạy test trong Visual Studio/Test Explorer
- FluentAssertions: assert dễ đọc và dễ bảo trì
- Microsoft.AspNetCore.Mvc.Testing: host API cho integration test qua WebApplicationFactory

Mục đích:
- Test API end-to-end hoặc gần end-to-end

### Tùy chọn

- Moq nếu cần mock một phần dependency: giả lập một số service ngoài khi test luồng tích hợp

## 10. Ghi chú migration

- Nếu dùng EF Core migration, cài thêm dotnet-ef dưới dạng global tool
- Không cần cài dotnet-ef như package trong project

## 11. Ghi chú thực tế

- Danh sách trên là bộ khởi đầu hợp lý cho dự án của bạn
- Khi phát sinh tính năng như email, caching, upload file, logging nâng cao thì mới thêm package tương ứng
