# Phân rã công việc cho Team Leader (Data Architect & Core API)

> **Lưu ý quan trọng:** Giai đoạn này tập trung vào thiết kế hệ thống và thống nhất quy trình, **CHƯA** tiến hành viết code ngay lập tức.

## 1. Thiết kế Database SQL Server (Sơ đồ ERD)
Đây là công việc ưu tiên số 1 vì nó là xương sống của toàn hệ thống.

- [x] **Task 1.1: Gom nhặt yêu cầu dữ liệu:** Xác định chính xác "Hồ sơ học viên" và "Thông tin cá nhân" cần lưu trữ những trường thông tin gì (Mã HV, Tên, Ngày sinh, Căn cước, Địa chỉ, Trạng thái học tập, v.v.).
- [x] **Task 1.2: Xác định các thực thể (Entities) và Mối quan hệ (Relationships):** Ngoài học viên, xác định các bảng liên kết (Ví dụ: Lớp học, Khóa học, Điểm số, Tài khoản đăng nhập). Xác định quan hệ 1-1, 1-N, N-N.
- [x] **Task 1.3: Chốt kiểu dữ liệu và Ràng buộc:** Lên danh sách kiểu dữ liệu tương ứng trong SQL Server (NVARCHAR, DATE, INT, UNIQUEIDENTIFIER, v.v.), chỉ định Khóa chính (PK) và Khóa ngoại (FK).
- [x] **Task 1.4: Vẽ sơ đồ ERD:** Sử dụng một công cụ như `dbdiagram.io`, `draw.io` hoặc `Visual Paradigm` để vẽ sơ đồ trực quan. Sơ đồ này cần được chốt với team trước khi tạo database vật lý.

## 2. Khởi tạo cấu trúc Project ban đầu
Chuẩn bị "bộ khung" vững chắc để các thành viên khác có thể bắt đầu làm việc.

- [x] **Task 2.1: Chốt Tech Stack & Kiến trúc:** Thống nhất framework backend (ASP.NET Core, Node.js, Spring Boot...) và kiến trúc phần mềm (MVC, Layered Architecture, Clean Architecture).
- [x] **Task 2.2: Lên danh sách thư viện cốt lõi (Dependencies):** Xác định trước các package ORM (Entity Framework, Dapper...), thư viện xác thực (JWT), thư viện Logging.
- [x] **Task 2.3: Viết tài liệu Coding Convention:** Tạo tài liệu quy định cách đặt tên biến, tên hàm, tên API, cách xử lý lỗi (try/catch), format response trả về để thống nhất trong team.

## 3. Chuẩn bị Xây dựng API cốt lõi
Áp dụng cách tiếp cận API-First, thiết kế API trước khi implement.

- [x] **Task 3.1: Định nghĩa danh sách Endpoints:** Lên danh sách các URL cần thiết (VD: `GET /api/v1/students`, `POST /api/v1/students`, `GET /api/v1/students/{id}`).
- [x] **Task 3.2: Thiết kế Request/Response Schema:** Xác định rõ cấu trúc JSON gửi lên và cấu trúc JSON trả về (bao gồm cả mã lỗi HTTP).
- [x] **Task 3.3: Viết tài liệu OpenAPI/Swagger (API Contract):** Viết file `swagger.yaml` hoặc `openapi.json` nháp. Việc này tạo Mock API để team App/Frontend làm giao diện ngay mà không cần chờ Backend code xong.

## 4. Quản lý Repository, Review Code & Workflow
Thiết lập môi trường làm việc chung cho cả team.

- [ ] **Task 4.1: Setup Git Repository:** Tạo repo trên GitHub/GitLab, thêm file `.gitignore` phù hợp với Tech Stack, thêm file `README.md` cơ bản.
- [ ] **Task 4.2: Thống nhất Git Workflow:** Lên quy định về cách tạo nhánh (VD: `feature/...`, `bugfix/...`) và cách viết Commit Message (VD: Conventional Commits).
- [ ] **Task 4.3: Thiết lập Branch Protection:** Cấu hình chặn push trực tiếp lên nhánh `main`/`develop`. Yêu cầu bắt buộc tạo Pull Request (PR) và phải có người Review trước khi merge.
- [ ] **Task 4.4: Phân quyền thành viên:** Mời các thành viên vào repository và set quyền phù hợp.
