# Phân rã công việc cho Backend Developer (Nguyễn Văn Thuyết)

> **Vai trò:** Business Logic API
> **Nhiệm vụ trọng tâm:** Xử lý các luồng nghiệp vụ lõi, tính toán và đảm bảo tính chính xác của dữ liệu.

## 1. Chuẩn bị & Hiểu nghiệp vụ
- [ ] **Task 1.1: Nghiên cứu DB Schema:** Phối hợp cùng Team Leader (Thi) để hiểu rõ cấu trúc Database (đặc biệt là bảng Điểm số, Lịch học, Điểm danh).
- [ ] **Task 1.2: Phân tích luồng Đăng ký khóa học:** Xác định các bước khi một học viên đăng ký (Kiểm tra điều kiện tiên quyết, số lượng tối đa, cập nhật trạng thái hóa đơn/học phí nếu có).
- [ ] **Task 1.3: Phân tích luồng Điểm danh & Nhập điểm:** Thống nhất cách lưu trữ trạng thái điểm danh (Có mặt, Vắng phép, Vắng không phép) và thang điểm.

## 2. Xây dựng API Nghiệp vụ
- [ ] **Task 2.1: API Đăng ký khóa học:** Viết endpoint cho phép học viên đăng ký khóa học, bao gồm các validation logic.
- [ ] **Task 2.2: API Điểm danh:** 
  - Điểm danh thủ công theo buổi.
  - Xử lý logic điểm danh bằng mã QR (nếu có yêu cầu từ App).
- [ ] **Task 2.3: API Nhập điểm:** Cho phép giảng viên/admin nhập điểm và tự động tính toán cập nhật vào bảng tổng hợp.

## 3. Xử lý Logic Tính Toán
- [ ] **Task 3.1: Viết logic tính tỷ lệ chuyên cần:** Tạo service riêng tính toán số buổi vắng / tổng số buổi, cảnh báo nếu vượt quá % cho phép.
- [ ] **Task 3.2: Viết logic tính điểm trung bình (GPA):** Viết service tính toán GPA tự động dựa trên trọng số các bài kiểm tra.

## 4. Kiểm thử chất lượng (Unit Test)
- [ ] **Task 4.1: Setup Unit Test framework:** Cài đặt xUnit/NUnit/Moq (nếu dùng C#) hoặc Jest (nếu dùng Node.js).
- [ ] **Task 4.2: Viết Unit Test cho luồng Điểm danh:** Đảm bảo không thể điểm danh 2 lần, hoặc điểm danh khi buổi học chưa diễn ra.
- [ ] **Task 4.3: Viết Unit Test cho luồng Tính điểm/Chuyên cần:** Đảm bảo phép toán cho ra kết quả chính xác với nhiều edge-cases khác nhau.
