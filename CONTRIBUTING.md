# 🤝 Hướng dẫn Đóng góp

Cảm ơn bạn quan tâm đến dự án Restaurant Management System. Dưới đây là hướng dẫn để bạn đóng góp một cách hiệu quả.

## 📋 Trước khi bắt đầu

1. **Fork** repository này
2. **Clone** fork của bạn
3. Tạo branch mới cho công việc của bạn

```bash
git checkout -b feature/tên-tính-năng
# hoặc
git checkout -b fix/tên-lỗi
```

## 🐛 Báo cáo lỗi

Khi bạn phát hiện lỗi, vui lòng:

1. Kiểm tra [Issues](https://github.com/your-user/RestaurantManagementSystem_Solo/issues) xem lỗi này đã được báo cáo chưa
2. Nếu chưa, tạo issue mới và chọn mẫu **"🐛 Báo cáo lỗi"**
3. Điền đầy đủ các thông tin:
   - Cách tái hiện lỗi từng bước
   - Hành vi hiện tại vs hành vi mong đợi
   - Các file logs hoặc stack trace nếu có
   - Thông tin môi trường (OS, .NET version, database, v.v.)

### Mẫu báo cáo lỗi
```
[BUG] Tiêu đề ngắn mô tả lỗi

📋 Mô tả lỗi:
- Chi tiết lỗi...

🔍 Cách tái hiện:
1. Bước 1...
2. Bước 2...
3. Bước 3...

📸 Hành vi hiện tại:
- Kết quả thực tế...

✅ Hành vi mong đợi:
- Kết quả nên là...

📝 Thông tin thêm:
- Logs, screenshots, stack trace nếu có
```

## ✨ Yêu cầu tính năng

Để đề xuất tính năng mới:

1. Kiểm tra [Issues](https://github.com/your-user/RestaurantManagementSystem_Solo/issues) xem tính năng này đã được đề xuất chưa
2. Tạo issue mới và chọn mẫu **"✨ Yêu cầu tính năng"**
3. Mô tả chi tiết:
   - Tính năng bạn muốn thêm
   - Vấn đề nó giải quyết
   - Cách bạn muốn nó hoạt động
   - Các giải pháp thay thế nếu có

### Mẫu yêu cầu tính năng
```
[FEATURE] Tiêu đề ngắn mô tả tính năng

📖 Mô tả tính năng:
- Chi tiết...

🎯 Vấn đề cần giải quyết:
- Vấn đề hiện tại...

💡 Giải pháp đề xuất:
- Cách hoạt động...

📝 Ví dụ sử dụng:
- Code example hoặc flow...
```

## 💻 Đóng góp code

### 1. Quy ước branch
- `feature/...` - tính năng mới
- `fix/...` - sửa lỗi
- `docs/...` - cập nhật tài liệu
- `refactor/...` - refactor code
- `test/...` - thêm test

### 2. Quy ước commit message
```
feat(module): mô tả tính năng mới
fix(module): mô tả sửa lỗi
docs(module): cập nhật tài liệu
test(module): thêm test
refactor(module): refactor code
```

Ví dụ:
```
feat(order): thêm API tạo order mới
fix(auth): sửa lỗi refresh token hết hạn
docs(next-steps): cập nhật roadmap
test(order): thêm unit test cho tính tiền
```

### 3. Quy ước code
Tuân theo [Backend Conventions](docs/backend-conventions.md):
- Dùng PascalCase cho class, method, property
- Dùng camelCase cho biến cục bộ, tham số
- Interface bắt đầu bằng `I`
- Hằng số dùng PascalCase
- Async method kết thúc bằng `Async`

### 4. Kiểm tra trước khi push
```bash
# Build solution
dotnet build

# Chạy unit test
dotnet test

# Chạy linter (nếu có)
dotnet format --verify-no-changes --verbosity diagnostic
```

## 📦 Pull Request

### Trước khi submit
1. Cập nhật `main` branch
   ```bash
   git fetch upstream
   git rebase upstream/main
   ```
2. Đảm bảo code build và test pass
3. Viết commit message rõ ràng theo quy ước

### Khi submit PR
1. Tiêu đề PR: `[TYPE] Mô tả ngắn` (vd: `[FEATURE] Thêm API tạo order`)
2. Mô tả chi tiết:
   - Tính năng / lỗi được giải quyết
   - Cách bạn giải quyết
   - Có thay đổi schema DB không? Nếu có, migration nào?
   - Test được viết chưa?
3. Liên kết issue liên quan: `Fixes #123`
4. Chụp screenshot nếu có thay đổi UI

### Mẫu PR
```markdown
## 📖 Mô tả
Giải quyết issue #123: [mô tả vấn đề]

## 🎯 Thay đổi
- Thay đổi 1
- Thay đổi 2

## 🧪 Testing
- [x] Build pass
- [x] Unit test pass
- [x] Integration test pass
- [x] Không có lỗi compile

## 📸 Screenshots (nếu có)
<!-- Thêm screenshot nếu có thay đổi UI -->

## 📝 Checklists
- [x] Tuân theo code convention
- [x] Đã cập nhật docs nếu cần
- [x] Không có breaking changes
- [ ] Thêm test cho thay đổi
```

## 📚 Tài liệu

Xem thêm:
- [Architecture](docs/architecture.md)
- [Backend Conventions](docs/backend-conventions.md)
- [Database Conventions](docs/db-conventions.md)
- [JWT Configuration](docs/jwt-configuration.md)
- [Next Steps](docs/next-steps.md)

## 🚀 Development Setup

### Backend
```bash
cd backend
dotnet restore
dotnet build
# Update database
dotnet ef database update

# Run
dotnet run --project src/RestaurantManagement.API
```

### Frontend
```bash
cd frontend
npm install
npm run dev
```

## 📋 Checklist Definition of Done

Trước khi PR được merge:
- [ ] Build pass
- [ ] Test pass
- [ ] Không có warning lỗi
- [ ] Tuân theo convention
- [ ] Docs được cập nhật nếu cần
- [ ] Không phá flow Order end-to-end
- [ ] Ít nhất 1 review approve

## 🎯 Quy trình Review

Sau khi submit PR:
1. Maintainer sẽ review code
2. Có thể yêu cầu thay đổi
3. Sau khi approve, PR sẽ được merge

## ❓ Câu hỏi?

Nếu bạn có câu hỏi:
- Tạo [Discussion](https://github.com/your-user/RestaurantManagementSystem_Solo/discussions) để hỏi chung
- Hoặc tìm kiếm trong [Issues](https://github.com/your-user/RestaurantManagementSystem_Solo/issues) xem có ai hỏi tương tự không

---

**Cảm ơn bạn đã đóng góp! 🙏**
