# Phân rã công việc cho DevOps & Integration (Ngọc Minh Kiên)

> **Vai trò:** DevOps, System Integration, Security
> **Nhiệm vụ trọng tâm:** Đóng gói ứng dụng, cấu hình Gateway, Auth (Validation) và Hạ tầng Microservices.

## 1. Môi trường Containerization (Docker)
- [x] **Task 1.1:** Viết `Dockerfile` tối ưu cho Backend API (Sử dụng multi-stage build để giảm dung lượng image).
- [x] **Task 1.2:** Viết `docker-compose.yml` để chạy đồng thời: SQL Server và Backend API.
- [x] **Task 1.3 (Tương lai):** Thêm **RabbitMQ** (Message Broker) vào `docker-compose.yml` để làm hạ tầng giao tiếp giữa các Nhóm.

## 2. Bảo mật và Xác thực (JWT Validation)
*
- [ ] **Task 2.3:** Viết custom middleware lấy `UserId` từ Claims của Token để truyền ngầm xuống tầng BLL (không bắt Frontend truyền qua body/query).
Lưu ý quan trọng: Nhóm 3 (Payment & Report) đảm nhận cấp phát JWT. Nhóm 2 chỉ làm phần Validate.*
- [ ] **Task 2.1:** Cấu hình Middleware `AddJwtBearer` để API Nhóm 2 hiểu và giải mã được Token do Nhóm 3 sinh ra (Dùng chung Secret Key).
- [ ] **Task 2.2:** Cấu hình Role-based Authorization: Gắn cờ `[Authorize(Roles="Admin,Teacher")]` cho API Điểm danh/Nhập điểm, và `[Authorize(Roles="Student")]` cho API tra cứu cá nhân.
## 3. Cấu hình Ocelot API Gateway
- [ ] **Task 3.1:** Thiết lập Gateway chung (hoặc độc lập). Cấu hình file `ocelot.json` để định tuyến các request `/student-svc/*` vào đúng API của Nhóm 2.
- [ ] **Task 3.2:** Cấu hình chuyển tiếp Token (Forward Authentication) từ Gateway xuống các Service con.

## 4. Hạ tầng Nâng cao (Định hướng tương lai)
- [ ] **Task 4.1:** Cấu hình **HealthChecks** để Gateway (hoặc Docker) biết Service Nhóm 2 có đang "sống" hay không.
- [ ] **Task 4.2:** Setup CI/CD cơ bản với **GitHub Actions** (Tự động build và chạy Unit Test khi có người tạo Pull Request vào nhánh `dev`).
- [ ] **Task 4.3:** Tích hợp **SignalR** để đẩy thông báo Real-time (Ví dụ: Server tự động bắn thông báo "Bạn vừa có điểm môn C#" về Mobile App).
