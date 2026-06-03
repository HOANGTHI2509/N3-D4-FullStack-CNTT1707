# Phân rã công việc cho Backend Developer (Nguyễn Văn Thuyết)

> **Vai trò:** Business Logic API (Nghiệp vụ cốt lõi)
> **Nhiệm vụ trọng tâm:** Xử lý các luồng đăng ký học, điểm danh, nhập điểm và tích hợp sự kiện (Event-driven).

## 1. API Đăng ký khóa học (Enrollments)
- [ ] **Task 1.1:** Viết API cho phép học viên đăng ký vào một lớp (`POST /api/v1/enrollments`). Đảm bảo kiểm tra trùng lặp dữ liệu (Một người không đăng ký 1 lớp 2 lần).
- [ ] **Task 1.2:** Viết API hủy đăng ký (Rút học phần) cho phép thay đổi trạng thái.
- [ ] **Task 1.3:** Viết API lấy danh sách học viên của một lớp cụ thể (để cung cấp cho giáo viên xem).

## 2. API Điểm danh (Attendances)
- [ ] **Task 2.1:** Viết API điểm danh hàng loạt cho một lớp trong một ngày (`POST /api/v1/attendances`).
- [ ] **Task 2.2:** Viết logic tính toán **Tỷ lệ % chuyên cần** của một học viên trong một lớp (`GET /api/v1/attendances/summary/{studentId}`).
- [ ] **Task 2.3 (Tương lai/Nâng cao):** Tích hợp luồng điểm danh bằng mã QR (Sinh mã QR chứa thông tin buổi học trên web và thiết kế API xử lý khi App quét mã).

## 3. API Điểm số (Grades)
- [ ] **Task 3.1:** Viết API nhập điểm (giữa kỳ, cuối kỳ) cho học viên (`POST /api/v1/grades`).
- [ ] **Task 3.2:** Viết logic tính Điểm trung bình môn (GPA) tự động trả về khi tra cứu.

## 4. Giao tiếp liên dịch vụ (Cross-Service Communication - Định hướng tương lai)
- [ ] **Task 4.1 (Event-Driven):** Phối hợp với Kiên cài đặt **RabbitMQ**. Viết Consumer để lắng nghe sự kiện `class.opened` từ Nhóm 1. Khi Nhóm 1 mở lớp mới, lưu thông tin ID của lớp đó về DB Nhóm 2.
- [ ] **Task 4.2 (Synchronous API):** Viết HTTP Client (hoặc gRPC) gọi sang API của Nhóm 1 để lấy "Tên Khóa học" hiển thị cho App học viên (vì DB Nhóm 2 chỉ lưu `ClassId`).
