# Phân rã công việc cho DevOps & Integration (Ngọc Minh Kiên)

> **Vai trò:** DevOps, System Integration, Security
> **Nhiệm vụ trọng tâm:** Đóng gói ứng dụng, cấu hình Gateway, Auth và Real-time.

## 1. Môi trường Containerization (Docker)
- [ ] **Task 1.1: Viết Dockerfile cho Backend API:** Build và đóng gói ứng dụng API vào container với image tối ưu nhẹ nhất có thể.
- [ ] **Task 1.2: Viết docker-compose.yml:** Cấu hình file compose để chạy đồng thời cả Database (SQL Server/MySQL) và Backend API chỉ bằng 1 lệnh (`docker-compose up`).
- [ ] **Task 1.3: Cấu hình biến môi trường (.env):** Tách biệt các chuỗi kết nối, config nhạy cảm ra khỏi source code để dễ dàng deploy qua các môi trường khác nhau.

## 2. Bảo mật và Xác thực (JWT Auth)
- [ ] **Task 2.1: Implement JWT Service:** Tích hợp logic sinh Token (Access Token, Refresh Token) khi User/Học viên đăng nhập.
- [ ] **Task 2.2: Cấu hình Role-based Authorization:** Phân quyền rõ ràng (Admin, Học viên, Giáo viên) trên các API route cụ thể.
- [ ] **Task 2.3: Bảo vệ API Endpoints:** Test thử việc gắn middleware xác thực vào các controller xem có block thành công các request trái phép hay không.

## 3. Cấu hình API Gateway (Ocelot)
- [ ] **Task 3.1: Khởi tạo Project API Gateway:** Cài đặt và cấu hình Ocelot (nếu dùng .NET) hoặc Kong/Nginx.
- [ ] **Task 3.2: Mapping Routes (Routing):** Chuyển tiếp các request từ App qua Gateway xuống đúng các Microservices/Controllers backend.
- [ ] **Task 3.3: Tích hợp Auth vào Gateway:** Chặn các request không có JWT hợp lệ ngay tại cổng Gateway trước khi chạm vào API lõi.

## 4. Nghiên cứu & Tích hợp Real-time (SignalR)
- [ ] **Task 4.1: POC SignalR:** Dựng một project nhỏ chứng minh khái niệm (Proof of Concept) việc đẩy thông báo từ Server xuống Client theo thời gian thực.
- [ ] **Task 4.2: Tích hợp SignalR vào API:** Áp dụng vào luồng "Thông báo cho học viên" hoặc "Cập nhật kết quả điểm danh".
- [ ] **Task 4.3: Viết tài liệu hướng dẫn kết nối SignalR:** Hướng dẫn cho team Frontend/Mobile App cách lắng nghe các sự kiện WebSocket/SignalR.
