# 🎓 Hệ thống Quản lý Khóa học & Học viên Trung tâm 
(Kế hoạch dự kiến cần chỉnh sửa thêm)

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)

Đây là kho lưu trữ mã nguồn cho **Student & Attendance Service (Phân hệ N2)**, một phần của dự án **Hệ thống Quản lý Trung tâm Đào tạo** (Đề tài 04) được thiết kế theo kiến trúc **Microservices**.
Dự án được xây dựng dưới dạng **Full-stack (bao gồm cả Web Admin và Mobile App)** nhằm mang lại trải nghiệm toàn diện cho người dùng (Học viên, Giáo viên và Admin).

---

## 👥 Đội ngũ thực hiện (Nhóm 2)
- 👨‍💻 **Hoàng Văn Thi** (Trưởng nhóm) - *Data Architect & Core API*
- 👨‍💻 **Nguyễn Văn Thuyết** - *Business Logic API*
- 👨‍💻 **Ngọc Minh Kiên** - *DevOps & Integration*

---

## 📋 Kế hoạch triển khai & Phân công nhiệm vụ

Để dự án chạy đúng tiến độ và chuyên nghiệp, tránh dẫm chân lên code của nhau, nhóm tuân thủ quy trình làm việc và phân công như sau:

### 1. Quy tắc quản lý mã nguồn (Git Flow)
Tuân thủ nghiêm ngặt quy tắc **"Nhánh độc lập - Tích hợp liên tục"**:
- **`main`**: Chỉ chứa code sạch, đã chạy hoàn hảo trên cả Web và App. **Tuyệt đối cấm commit trực tiếp.**
- **`dev`**: Nhánh trung gian tích hợp. Tất cả các thành viên đẩy code vào đây để test chung, đảm bảo API ghép với DB không phát sinh lỗi.
- **`feature/*` (Nhánh tính năng):** Mỗi khi làm một API hay chức năng mới, bắt buộc phải tạo nhánh mới từ `dev`.
  - *Ví dụ:* `feature/thi-api-student`, `feature/thuyet-api-attendance`, `feature/kien-docker-setup`.
- **Quy trình chuẩn:** Code trên nhánh cá nhân ➔ Test bằng Postman/Swagger thành công ➔ Tạo Pull Request (PR) vào `dev` ➔ Review chéo ➔ Merge.

### 2. Phân công nhiệm vụ cụ thể

| Thành viên | Trách nhiệm chính | Công việc cụ thể |
| :--- | :--- | :--- |
| **Hoàng Văn Thi** *(Team Leader)* | **Data Architect & Core API** | - Thiết kế Database SQL Server (Sơ đồ ERD chuẩn).<br>- Khởi tạo cấu trúc Project ban đầu.<br>- Xây dựng API cốt lõi: CRUD Hồ sơ học viên, API tra cứu thông tin cá nhân (phục vụ App).<br>- Quản lý Repository, review code và xử lý Conflict khi merge. |
| **Nguyễn Văn Thuyết** | **Business Logic API** | - Xây dựng API nghiệp vụ phức tạp: Đăng ký khóa học, Điểm danh (theo buổi/theo mã QR), Nhập điểm.<br>- Viết logic tính toán: Tỷ lệ % chuyên cần, điểm trung bình.<br>- Viết Unit Test cơ bản cho các luồng logic khó. |
| **Ngọc Minh Kiên** | **DevOps & Integration** | - Đóng gói Service vào Docker Container.<br>- Xác thực JWT Auth (Validation - không cấp phát) do Nhóm 3 cấp để phân quyền.<br>- Cấu hình Ocelot API Gateway và nghiên cứu SignalR. |

### 3. Lộ trình thực hiện (Roadmap)
- **Giai đoạn 1 (Thiết kế & Base Project):** Chốt cấu trúc Database (Thi) và đẩy khung Project chuẩn lên nhánh `dev`.
- **Giai đoạn 2 (Dev API & Infras):**
  - Thi và Thuyết tách nhánh `feature/` bắt đầu viết Controllers, Services, Repositories cho từng chức năng.
  - Kiên setup Docker, cấu hình API Gateway và JWT Auth song song.
- **Giai đoạn 3 (Tích hợp):** Cuối mỗi tuần, đẩy các nhánh `feature` hoàn thiện về `dev`. Mở Swagger test chéo API của nhau.
- **Giai đoạn 4 (Bàn giao):** Merge vào `main`, cung cấp tài liệu API (Endpoint, Body, Response) đầy đủ cho team Frontend.

---

## 💡 Các nguyên tắc cốt lõi trong phát triển
1. **Dùng chung Model/DTO tối ưu cho Mobile App:** Dữ liệu trả về cho App phải được tối ưu, đóng gói gọi nhẹ qua DTO (chỉ lấy những trường cần thiết), tránh bắt App phải load toàn bộ dữ liệu cồng kềnh của Web.
2. **Không Query vượt cấp:** API của ai người nấy viết, dùng chung DB context nhưng tuyệt đối tuân thủ nguyên tắc SOLID.
3. **Dữ liệu độc lập (Microservices):** Tuyệt đối không lưu dư thừa bảng của service khác (VD: Không lưu Accounts của N3, không lưu Courses của N1). Mọi giao tiếp phải thông qua API Gateway, HTTP Client hoặc RabbitMQ.
