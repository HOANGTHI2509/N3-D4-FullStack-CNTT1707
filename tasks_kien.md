# Phân rã công việc cho DevOps & Integration (Ngọc Minh Kiên)

> **Vai trò:** DevOps, System Integration
> **Nhiệm vụ trọng tâm:** Đóng gói ứng dụng, cấu hình hạ tầng Microservices nội bộ và tiếp nhận dữ liệu từ Gateway.

## 1. Môi trường Containerization (Docker)
- [x] **Task 1.1:** Viết `Dockerfile` tối ưu cho Backend API (Sử dụng multi-stage build để giảm dung lượng image).
- [x] **Task 1.2:** Viết `docker-compose.yml` để chạy đồng thời: SQL Server và Backend API.
- [x] **Task 1.3 (Tương lai):** Thêm **RabbitMQ** (Message Broker) vào `docker-compose.yml` để làm hạ tầng giao tiếp giữa các Nhóm.

## 2. Giao tiếp với API Gateway (Xử lý Request nội bộ)
*Lưu ý quan trọng: Ocelot API Gateway (điểm vào duy nhất) đã đảm nhận việc xác thực JWT do Nhóm 3 cấp phát. Khi request lọt được vào Service Nhóm 2, nó đã an toàn. Nhóm 2 KHÔNG cần validate JWT.*
- [ ] **Task 2.1:** Viết Custom Middleware hoặc ActionFilter để tự động đọc `UserId` và `Role` từ HTTP Headers (ví dụ: `X-User-Id`, `X-User-Role`) do Ocelot Gateway trích xuất từ Token và truyền xuống.
- [ ] **Task 2.2:** Áp dụng Authorization nội bộ dựa trên Role lấy từ Header (Gắn quyền `Admin/Teacher` cho API nhập điểm, `Student` cho tra cứu).
- [ ] **Task 2.3:** Tiêm (Inject) thông tin `UserId` lấy từ Header vào tầng BLL để lưu thông tin ai là người thay đổi bản ghi (Audit Log).

## 3. Hạ tầng Nâng cao (Định hướng tương lai)
- [ ] **Task 3.1:** Cấu hình **HealthChecks** để cung cấp endpoint `/health` cho Gateway biết Service Nhóm 2 có đang "sống" hay không.
- [ ] **Task 3.2:** Setup CI/CD cơ bản với **GitHub Actions** (Tự động build và chạy Unit Test khi có người tạo Pull Request vào nhánh `dev`).
- [ ] **Task 3.3:** Tích hợp **SignalR** để đẩy thông báo Real-time (Ví dụ: Server tự động bắn thông báo "Bạn vừa có điểm môn C#" về Mobile App).
