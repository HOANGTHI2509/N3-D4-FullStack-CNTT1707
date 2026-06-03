# Tiêu chuẩn lập trình (Coding Conventions) & Kiến trúc

Tài liệu này quy định các chuẩn mực chung khi viết code trong dự án **Smart Campus (Core Business - B6)** để đảm bảo tính nhất quán, dễ đọc, và dễ bảo trì.

## 1. Kiến trúc phân tầng (N-Tier / Layered)
Dự án được chia thành các project con:
- **`SmartCampus` (API Layer):** Chỉ chứa Controllers, Middlewares, và cấu hình DI (Dependency Injection). Tuyệt đối không chứa logic tính toán hay truy vấn Database trực tiếp.
- **`SmartCampus.BLL` (Business Logic Layer):** Chứa các Services xử lý nghiệp vụ (ví dụ: tính chuyên cần, logic đăng ký khóa học). Tầng này nhận dữ liệu từ Controller và gọi xuống DAL.
- **`SmartCampus.DAL` (Data Access Layer):** Chứa DbContext, Entity Models, và Repositories để tương tác trực tiếp với SQL Server.

## 2. Chuẩn đặt tên (Naming Conventions)
- **Tên Class, Record, Struct, Enum:** `PascalCase` (Ví dụ: `StudentService`, `EnrollmentStatus`).
- **Tên Interface:** Bắt đầu bằng chữ `I` và viết `PascalCase` (Ví dụ: `IStudentRepository`).
- **Tên tham số (Parameters) và biến cục bộ:** `camelCase` (Ví dụ: `studentId`, `fullName`).
- **Tên hằng số (Constants):** `PascalCase` (Ví dụ: `MaxPageSize`).
- **Tên biến private trong class:** Bắt đầu bằng dấu `_` và `camelCase` (Ví dụ: `_studentRepository`).

## 3. Chuẩn trả về của API (API Response Format)
Tất cả các API khi trả dữ liệu về cho Client (Web/App) **BẮT BUỘC** phải bọc trong một format JSON thống nhất như sau, giúp App/Frontend dễ dàng xử lý (parse) dữ liệu:

```json
{
  "success": true,               // true hoặc false
  "message": "Thành công",       // Lời nhắn thân thiện cho user
  "data": {                      // Dữ liệu chính (object hoặc array)
     "id": 1,
     "name": "Hoàng Văn Thi"
  },
  "errors": null                 // Chi tiết danh sách lỗi (chỉ có khi success = false)
}
```

## 4. Quản lý Lỗi (Exception Handling)
- Hạn chế dùng `try-catch` thủ công ở mọi hàm trong Controller. Thay vào đó, xây dựng một **Global Exception Middleware** (trong tầng API) để "bắt" toàn bộ lỗi.
- Tránh trả về trực tiếp Exception StackTrace (lỗi 500 nguyên bản của C#) cho Client để đảm bảo bảo mật.

## 5. DTO (Data Transfer Objects)
- Tuyệt đối không trả thẳng Entity (Models được sinh ra từ Database) về phía Client. 
- Mọi dữ liệu trả về hoặc nhận vào từ Client phải thông qua các file DTO (Ví dụ: `StudentDto`, `CreateStudentRequest`). Dùng AutoMapper để ánh xạ từ Entity sang DTO và ngược lại.
