# Hướng dẫn trình tự thực hiện dự án (Project Workflow)

Tài liệu này hướng dẫn cách nhóm phối hợp làm việc theo từng giai đoạn, dựa trên roadmap đã chốt, đảm bảo không ai bị block và code không bị conflict.

## Giai đoạn 1: Chuẩn bị & Đặt móng (Khoảng 2-3 ngày)
*Mục tiêu: Chốt kiến trúc, DB và các team viên có môi trường làm việc.*

1. **(Thi)** Thiết kế sơ đồ ERD hoàn chỉnh và chốt với Thuyết.
2. **(Thi)** Tạo project gốc, setup cấu trúc thư mục, khởi tạo repo GitHub/GitLab và push nhánh `main`, sau đó tạo nhánh `dev`.
3. **(Kiên)** Pull code nhánh `dev` về, bắt tay vào viết `docker-compose.yml` để dựng Database và môi trường lên.
4. **(Thi)** Viết quy định code (Coding conventions) và API Response chuẩn.

## Giai đoạn 2: Phát triển song song (Khoảng 1-2 tuần)
*Mục tiêu: Các thành viên làm việc độc lập trên các nhánh `feature`.*

- **Nhánh `feature/auth-and-gateway` (Kiên):**
  - Implement JWT.
  - Setup Ocelot API Gateway.
- **Nhánh `feature/core-api` (Thi):**
  - CRUD Học viên, User.
  - Khởi tạo Data Seeding (dữ liệu mẫu).
- **Nhánh `feature/business-logic` (Thuyết):**
  - (Cần đợi Thi hoàn thành ERD và Entity Models) Viết API Đăng ký, Điểm danh, Tính điểm.
  - Viết Unit Tests cho các hàm tính toán của mình.

**Quy tắc trong giai đoạn này:**
- Cuối mỗi ngày, đẩy code lên nhánh `feature/` của mình.
- Tuyệt đối không tự ý sửa file chung như `Program.cs` hay `appsettings.json` mà chưa báo cho cả nhóm. Nếu cần sửa, phải báo lên group chat.

## Giai đoạn 3: Tích hợp & Kiểm thử chéo (Hàng tuần)
*Mục tiêu: Đảm bảo code của Thi, Thuyết và Kiên chạy mượt mà khi ghép lại.*

1. **Tạo Pull Request (PR):**
   - Thi, Thuyết, Kiên tạo PR từ nhánh `feature` vào nhánh `dev`.
2. **Review Code (Thi chủ trì):**
   - Thi review code của Thuyết và Kiên.
   - Nếu có conflict ở các file cấu hình, Thi sẽ xử lý thủ công (Resolve Conflict).
3. **Merge và Test (Kiên & Thuyết):**
   - Sau khi merge vào `dev`, Kiên chạy lại toàn bộ bằng Docker.
   - Thuyết dùng Postman/Swagger bắn thử các API chạy qua Gateway (của Kiên) vào Core (của Thi).
4. **Fix Bug ngay trên `dev` hoặc tạo nhánh `bugfix/`** nếu lỗi nghiêm trọng.

## Giai đoạn 4: Hoàn thiện tài liệu & Bàn giao (1 tuần cuối)
*Mục tiêu: Giao API sạch, có tài liệu để Frontend/App tích hợp.*

1. **(Thi)** Kiểm tra lại Swagger/OpenAPI docs đã đủ comment, rõ ràng request/response chưa.
2. **(Kiên)** Kiểm tra lại các endpoint có bị lọt qua JWT (bảo mật) không.
3. **(Thuyết)** Chạy lại bộ Unit Test một lần cuối để đảm bảo pass 100%.
4. **(Team Leader)** Merge code từ nhánh `dev` sang nhánh `main` và đánh tag version Release (VD: `v1.0.0`). Cung cấp Postman Collection cho Frontend.
