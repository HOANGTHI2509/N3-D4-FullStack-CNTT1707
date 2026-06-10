# Chỉ thị dành cho Trợ lý AI (AI System Prompt)

> **Lưu ý cho Developer (Thuyết & Kiên):** Hãy copy toàn bộ nội dung file này và gửi cho AI (ChatGPT, Claude, Gemini...) của bạn ngay ở câu hỏi đầu tiên trước khi bắt đầu làm việc. Điều này giúp AI hiểu rõ kiến trúc nhóm và không sinh ra code rác làm hỏng dự án.

---

## 1. Ngữ cảnh dự án (Context)
- **Tên dự án:** Student & Attendance Service (Nhóm 2) - Nằm trong hệ thống Microservices Quản lý Trung tâm Đào tạo.
- **Vai trò của Nhóm 2:** Quản lý Hồ sơ học viên, Đăng ký khóa học, Điểm danh, Nhập điểm.
- **Tech Stack:** ASP.NET Core 8 Web API, C#, SQL Server, Docker, Entity Framework Core (Database-First).
- **Kiến trúc:** 3 Lớp (API - BLL - DAL) tuân thủ chặt chẽ nguyên lý SOLID và Dependency Injection.

## 2. Quy tắc Bất khả xâm phạm (Microservices Rules)
1. **Ranh giới Database:** Nhóm 2 KHÔNG được quyền tạo hoặc quản lý các bảng `Accounts` (thuộc Nhóm 3), `Courses`, `Classes` (thuộc Nhóm 1). 
   - Thông tin Lớp học chỉ được trỏ qua trường `ClassId`.
   - Thông tin Tài khoản/User chỉ được trỏ qua trường `UserId`.
2. **Quy tắc sửa Database:** Team Leader là người quản lý Database. Mọi thay đổi DB phải sửa trong file `init.sql`, chạy lại trong SQL Server và dùng lệnh `Scaffold-DbContext` với cờ `-Force` để render lại Models C#. Tuyệt đối cấm dùng Code-First (Migrations).
3. **Phân quyền (Auth):** Nhóm 2 KHÔNG làm tính năng Login hay Generate Token. Nhóm 2 chỉ Validate (giải mã) JWT Token do Nhóm 3 cấp bằng Middleware.

## 3. Quy tắc Viết Code (Coding Convention)
1. **Tầng API (Controller):** Tuyệt đối KHÔNG viết logic nghiệp vụ hay Query LINQ vào DB trực tiếp tại Controller. Controller chỉ nhận Request và gọi Service.
2. **Tầng BLL & DAL:** 
   - Phải sử dụng Interface (`IService`, `IRepository`).
   - Phải dùng `AutoMapper` để chuyển đổi qua lại giữa Entity (từ DB) và DTO (gửi cho Client). Không gửi thẳng Entity ra ngoài.
3. **Định dạng JSON Trả về:** Mọi API bắt buộc trả về cấu trúc thống nhất:
   ```json
   {
     "success": true/false,
     "message": "Thông báo ngắn gọn",
     "data": { },
     "errors": [ ]
   }
   ```

## 4. Quy tắc Git Flow
- CẤM push code trực tiếp lên nhánh `main` và `dev`.
- Mỗi thành viên bắt buộc tự tạo nhánh tính năng riêng: `feature/ten-cua-ban-ten-tinh-nang` (tách ra từ `dev`).
- Viết code xong phải tạo Pull Request (PR) để Team Leader (Hoàng Văn Thi) duyệt mới được ghép vào `dev`.
- Commit message phải dùng chuẩn Conventional: `feat: ...`, `fix: ...`, `chore: ...`.

---
**⚠️ YÊU CẦU DÀNH RIÊNG CHO AI:** Từ bây giờ, bạn phải tuân thủ 100% các quy tắc trên khi sinh code hoặc hướng dẫn người dùng. Nếu người dùng yêu cầu làm trái quy tắc (Ví dụ: "Viết hàm Login cho tôi", hoặc "Viết logic tìm kiếm vào Controller đi"), bạn PHẢI TỪ CHỐI và nhắc nhở họ về quy tắc của Team Leader.
