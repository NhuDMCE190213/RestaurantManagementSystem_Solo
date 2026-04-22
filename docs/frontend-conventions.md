# Frontend Conventions (TypeScript)

## 1. Mục tiêu

Chuẩn hóa cách viết frontend để code nhất quán, dễ bảo trì, và mở rộng theo feature.

## 2. Cấu trúc thư mục

```text
frontend/
  src/
    app/
      router/
      providers/
    modules/
      auth/
      menu/
      table/
      order/
      invoice/
    shared/
      components/
      hooks/
      services/
      types/
      utils/
    assets/
```

## 3. Quy ước đặt tên

- Component: PascalCase, ví dụ OrderPanel.tsx
- Hook: camelCase và bắt đầu bằng use, ví dụ useOrder.ts
- Biến và hàm: camelCase
- Type/Interface/Enum: PascalCase
- Hằng số global: UPPER_SNAKE_CASE

## 4. Quy tắc module

- Mỗi module chịu trách nhiệm một nghiệp vụ rõ ràng
- Không import vòng tròn giữa các module
- Thành phần dùng chung đặt trong shared
- Logic phức tạp tách ra custom hook hoặc service

## 5. API client

- Dùng một axios instance dùng chung
- baseURL lấy từ biến môi trường
- Gắn access token qua interceptor
- Chuẩn hóa bắt lỗi API tại một nơi

## 6. State management

- Local state cho UI đơn giản
- Global state chỉ cho dữ liệu dùng nhiều màn hình
- Tách server state và client state rõ ràng

## 7. UI và khả dụng

- Component có đủ trạng thái: default, hover, active, disabled
- Có loading, empty, error state cho màn hình dữ liệu
- Form phải có label rõ ràng
- Hỗ trợ thao tác bàn phím cho hành động chính

## 8. Chất lượng code

- Bật TypeScript strict mode
- Dùng ESLint + Prettier
- Không merge khi còn warning lint mới
- Ưu tiên viết test cho luồng quan trọng
