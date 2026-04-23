# Restaurant Management System (Solo)

Hệ thống quản lý nhà hàng xây dựng theo mô hình Fullstack:
- Backend: ASP.NET Core 9 (C#), Clean Architecture, JWT Authentication
- Frontend: TypeScript
- Quản lý mã nguồn: GitHub (1 người làm)

## 1. Mục tiêu dự án

Xây dựng hệ thống quản lý nhà hàng phục vụ các nghiệp vụ cơ bản:
- Quản lý món ăn, danh mục, giá bán
- Quản lý bàn, khu vực, trạng thái bàn
- Tạo và quản lý hóa đơn
- Quản lý đơn hàng tại chỗ/online
- Phân quyền người dùng (Admin/Staff)
- Báo cáo doanh thu cơ bản

## 2. Công nghệ sử dụng

### Backend
- .NET 9 / ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Bearer Authentication
- Swagger/OpenAPI
- xUnit + FluentAssertions (unit test)

### Frontend
- TypeScript
- Framework đề xuất: React + Vite (TypeScript)
- Axios (gọi API)
- React Router
- State management: Redux Toolkit hoặc Zustand

### DevOps / Quản lý mã nguồn
- Git + GitHub
- GitHub Projects (quản lý task)
- GitHub Issues / Pull Requests
- GitHub Actions (CI: build + test)

## 3. Kiến trúc tổng thể

Dự án theo Clean Architecture:

```text
backend/
	src/
		RestaurantManagement.Domain/         # Entities, Enums, Domain Events
		RestaurantManagement.Application/    # Use Cases, DTOs, Interfaces, Validators
		RestaurantManagement.Infrastructure/ # EF Core, Repositories, External Services
		RestaurantManagement.API/            # Controllers, Middleware, JWT, Swagger
	tests/
		RestaurantManagement.UnitTests/
		RestaurantManagement.IntegrationTests/

frontend/
	src/
		app/         # app config, router
		modules/     # feature-based modules (menu, table, order, invoice)
		shared/      # components, hooks, utils, types
```

Nguyên tắc:
- `Domain` không phụ thuộc vào layer khác.
- `Application` chỉ phụ thuộc `Domain`.
- `Infrastructure` và `API` phụ thuộc `Application`.
- Tất cả dependency hướng vào trong (Dependency Rule).

## 4. Chức năng dự kiến (MVP)

- Authentication + Authorization bằng JWT
- Quản lý người dùng và vai trò
- Quản lý danh mục + món ăn
- Quản lý bàn và đặt bàn cơ bản
- Tạo đơn hàng/hóa đơn, tính tổng tiền
- Lịch sử hóa đơn
- Dashboard doanh thu cơ bản

## 5. JWT và bảo mật

- Đăng nhập trả về `access token` (JWT)
- Refresh token (khuyến nghị) để gia hạn phiên đăng nhập
- Password hash bằng BCrypt/ASP.NET Identity PasswordHasher
- CORS cấu hình theo environment
- Hạn chế API theo role (Admin/Staff)
- Không commit secret lên GitHub (`appsettings.Development.json`, `.env`)

## 6. Quy ước quản lý GitHub (1 người làm)

Nhanh và gọn cho solo dev:

- Nhánh chính: `main`
- Nhánh phát triển: `dev`
- Nhánh tính năng: `feature/<ten-tinh-nang>`
- Nhánh sửa lỗi: `fix/<ten-loi>`

Quy trình đề xuất:
1. Tạo issue cho mỗi task.
2. Tạo branch từ `dev`.
3. Commit theo convention.
4. Mở Pull Request về `dev`.
5. Tự review checklist trước khi merge.
6. Merge `dev` -> `main` khi release.

Commit convention:
- `feat:` thêm tính năng
- `fix:` sửa lỗi
- `refactor:` cải tiến mã không đổi behavior
- `test:` bổ sung/sửa test
- `docs:` cập nhật tài liệu
- `chore:` việc hệ thống/cấu hình

Ví dụ:
```bash
feat(auth): add JWT login endpoint
fix(order): correct total amount calculation
docs(readme): update setup guide
```

## 7. Hướng dẫn khởi tạo nhanh

### 7.1. Tạo backend ASP.NET Core 9

```bash
dotnet new sln -n RestaurantManagementSystem
dotnet new webapi -n RestaurantManagement.API
dotnet new classlib -n RestaurantManagement.Domain
dotnet new classlib -n RestaurantManagement.Application
dotnet new classlib -n RestaurantManagement.Infrastructure
dotnet sln add **/*.csproj
```

Thêm tham chiếu theo Clean Architecture:
- Application -> Domain
- Infrastructure -> Application
- API -> Application + Infrastructure

### 7.2. Tạo frontend TypeScript

```bash
npm create vite@latest frontend -- --template react-ts
cd frontend
npm install
```

### 7.3. Chạy dự án

Backend:
```bash
cd backend
dotnet restore
dotnet build
dotnet run --project src/RestaurantManagement.API
```

Frontend:
```bash
cd frontend
npm install
npm run dev
```

## 8. Biến môi trường mẫu

Backend (`appsettings.Development.json` hoặc `.env`):

```json
{
	"ConnectionStrings": {
		"DefaultConnection": "Server=.;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True"
	},
	"Jwt": {
		"Issuer": "RestaurantManagementSystem",
		"Audience": "RestaurantManagementSystemClient",
		"Key": "YOUR_SUPER_SECRET_KEY_AT_LEAST_32_CHARS",
		"AccessTokenMinutes": 60
	}
}
```

Frontend (`.env`):

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

## 9. Testing và chất lượng

- Unit test cho Application layer
- Integration test cho API quan trọng (Auth, Order, Billing)
- Bắt buộc pass test trước khi merge
- Dùng format/lint trước khi push

## 10. Lộ trình đề xuất

- Milestone 1: Setup project + auth JWT + user roles
- Milestone 2: Menu + category + table management
- Milestone 3: Order + invoice flow
- Milestone 4: Dashboard + report + hardening

## 11. Tài liệu

- API docs: Swagger tại `/swagger`
- Kiến trúc chi tiết: `docs/architecture.md`
- Quy trình phát triển: `docs/development-workflow.md`
- Convention Backend: `docs/backend-conventions.md`
- Convention Frontend: `docs/frontend-conventions.md`
- Convention Database: `docs/db-conventions.md`
- Package matrix: `docs/packages.md`
- Quy trình GitHub: `docs/git-workflow.md`
- Công cụ sử dụng: `docs/tooling.md`
- UX flow: `docs/ux-flow.md`
- UI components checklist: `docs/ui-components-checklist.md`
- UI style guide: `docs/ui-style-guide.md`

## 12. Tác giả

- Solo Developer: NhuDMCE190213
- GitHub: [https://github.com/NhuDMCE190213](https://github.com/NhuDMCE190213)