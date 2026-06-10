# Bao cao kiem tra cong viec - Nguyen Van Thuyet

Ngay kiem tra: 10/06/2026

Cap nhat sau nang cap: Da bo sung bulk attendance, endpoint summary chuyen can rieng, constants cho status, unit test Attendance/Grade, va cap nhat them endpoint vao `swagger.yaml`.

## 1. Ket luan nhanh

Phan core cua Thuyet da lam duoc cac muc chinh:

- API dang ky lop/khoa hoc.
- API huy dang ky.
- API lay danh sach hoc vien theo lop.
- API diem danh.
- API nhap diem.
- API xem diem danh/diem theo lop va theo hoc vien.
- Logic tinh ty le chuyen can.
- Logic tinh diem trung binh.
- Logic xep loai ket qua cuoi khoa.
- Unit test cho logic dang ky va tinh ket qua.
- Unit test cho AttendanceService va GradeService.

Trang thai ky thuat hien tai:

- `dotnet build`: Pass, 0 error.
- `dotnet test`: Pass, 12/12 tests.

Ket luan: Co the bao cao la phan nghiep vu chinh da co ban hoan thanh, nhung van nen nang cap them mot so muc de dung sat task goc va de demo tot hon.

## 2. Nhung phan da hoan thanh

### 2.1. Enrollment API

Da co cac endpoint:

- `POST /api/v1/enrollments`
- `PATCH /api/v1/enrollments/{id}/cancel`
- `GET /api/v1/enrollments/classes/{classId}/students`

Da co logic:

- Kiem tra hoc vien ton tai truoc khi dang ky.
- Khong cho dang ky trung lop dang hoc.
- Cho dang ky lai neu dang ky cu da bi huy.
- Huy dang ky bang cach doi status.

File lien quan:

- `StudentManagement/SmartCampus/Controllers/EnrollmentsController.cs`
- `StudentManagement/SmartCampus.BLL/Services/EnrollmentService.cs`
- `StudentManagement/SmartCampus.DAL/Repositories/EnrollmentRepository.cs`

### 2.2. Attendance API

Da them cac endpoint:

- `POST /api/v1/attendances`
- `PUT /api/v1/attendances/{id}`
- `GET /api/v1/attendances/classes/{classId}`
- `GET /api/v1/attendances/students/{studentId}/classes/{classId}`

Da co logic:

- Kiem tra hoc vien ton tai.
- Kiem tra hoc vien da dang ky lop truoc khi diem danh.
- Khong cho diem danh trung cung hoc vien, cung lop, cung ngay.
- Cap nhat trang thai diem danh.
- Validate trang thai diem danh.

File lien quan:

- `StudentManagement/SmartCampus/Controllers/AttendancesController.cs`
- `StudentManagement/SmartCampus.BLL/Services/AttendanceService.cs`
- `StudentManagement/SmartCampus.DAL/Repositories/AttendanceRepository.cs`

### 2.3. Grade API

Da them cac endpoint:

- `POST /api/v1/grades`
- `PUT /api/v1/grades/{id}`
- `GET /api/v1/grades/classes/{classId}`
- `GET /api/v1/grades/students/{studentId}/classes/{classId}`

Da co logic:

- Kiem tra hoc vien ton tai.
- Kiem tra hoc vien da dang ky lop truoc khi nhap diem.
- Validate diem nam trong khoang 0 den 10.
- Cap nhat diem.

File lien quan:

- `StudentManagement/SmartCampus/Controllers/GradesController.cs`
- `StudentManagement/SmartCampus.BLL/Services/GradeService.cs`
- `StudentManagement/SmartCampus.DAL/Repositories/GradeRepository.cs`

### 2.4. Academic Result API

Da them endpoint:

- `GET /api/v1/results/students/{studentId}/classes/{classId}`

Da co logic:

- Tinh tong so buoi diem danh.
- Tinh so buoi duoc tinh la co tham gia.
- Tinh phan tram chuyen can.
- Tinh diem trung binh.
- Xep loai: `Gioi`, `Kha`, `Trung binh`, `Khong dat`.
- Dat neu chuyen can >= 80% va diem trung binh >= 5.

File lien quan:

- `StudentManagement/SmartCampus/Controllers/StudentResultsController.cs`
- `StudentManagement/SmartCampus.BLL/Services/AcademicResultCalculator.cs`
- `StudentManagement/SmartCampus.BLL/Services/StudentResultService.cs`

### 2.5. Unit Test

Da co test cho:

- Dang ky thanh cong.
- Hoc vien khong ton tai.
- Dang ky trung.
- Huy dang ky.
- Tinh chuyen can va diem trung binh.
- Khong dat do chuyen can thap.
- Khong dat khi chua co diem.

Ket qua:

- Tong test: 7.
- Passed: 7.
- Failed: 0.

File lien quan:

- `StudentManagement/SmartCampus.BLL.Tests/EnrollmentServiceTests.cs`
- `StudentManagement/SmartCampus.BLL.Tests/AcademicResultCalculatorTests.cs`

## 3. Nhung phan chua hoan thanh hoac nen nang cap

### 3.1. Diem danh hang loat cho ca lop

Task goc ghi: `POST /api/v1/attendances` la diem danh hang loat cho mot lop trong mot ngay.

Da nang cap endpoint:

- `POST /api/v1/attendances/bulk`

Endpoint nay cho phep gui danh sach hoc vien kem status trong mot request.

Trang thai: Da hoan thanh.

### 3.2. Endpoint summary rieng cho chuyen can

Task goc co goi y: `GET /api/v1/attendances/summary/{studentId}`.

Truoc do phan tinh chuyen can nam trong endpoint ket qua hoc tap:

- `GET /api/v1/results/students/{studentId}/classes/{classId}`

Da nang cap endpoint rieng:

- `GET /api/v1/attendances/summary/students/{studentId}/classes/{classId}`

Trang thai: Da hoan thanh.

### 3.3. Swagger YAML thu cong chua cap nhat

Project co Swagger UI sinh tu controller nen endpoint moi co the hien trong Swagger runtime.

Da bo sung them cac endpoint quan trong vao `swagger.yaml`:

- Enrollment.
- Bulk Attendance.
- Attendance Summary.
- Grade.
- Result.

Trang thai: Da cap nhat co ban. Neu can nop tai lieu dep hon, nen chuan hoa lai encoding tieng Viet trong toan bo file.

### 3.4. Chua co QR attendance

Task QR duoc ghi la tuong lai/nang cao.

Hien tai chua co:

- Sinh ma QR/session code.
- API nhan QR token khi App quet.
- Validate thoi gian het han cua QR.

Nen nang cap neu can demo nang cao:

- Tao bang/session tam cho buoi diem danh.
- Endpoint goi y:
  - `POST /api/v1/attendance-sessions`
  - `POST /api/v1/attendances/qr-check-in`

Do uu tien: Thap den trung binh, tuy muc tieu demo.

### 3.5. Chua tich hop Service Nhom 1

Hien tai DB chi luu `ClassId`, dung theo huong microservice.

Chua co:

- HTTP Client goi Course/Class Service cua Nhom 1.
- Kiem tra `ClassId` co ton tai khong.
- Lay ten lop/ten khoa hoc de tra ve cho App.

Nen nang cap:

- Tao `ICourseCatalogClient`.
- Cau hinh base URL trong `appsettings.json`.
- Khi dang ky, goi sang Nhom 1 de validate class.

Do uu tien: Trung binh den cao neu cac nhom da co API ghep noi.

### 3.6. Chua co RabbitMQ/Event-driven

Task 4.1 la dinh huong tuong lai.

Chua co:

- RabbitMQ consumer.
- Lang nghe event `class.opened`.
- Luu/cache thong tin lop mo moi.

Do uu tien: Thap neu du an chi can demo API co ban.

### 3.7. Status dang dung string thuong

Da tach constants:

- `EnrollmentStatuses`
- `AttendanceStatuses`

Trang thai: Da hoan thanh co ban.


### 3.8. Encoding tieng Viet trong repo dang bi loi

Nhieu file goc nhu `README.md`, `tasks_thuyet.md`, `init.sql`, `swagger.yaml` dang hien tieng Viet bi loi encoding.

Trong code moi da tam dung message ASCII de tranh loi hien thi.

Nen nang cap:

- Chuan hoa file ve UTF-8.
- Sua cac message tieng Viet bi loi.
- Sau khi sua, build/test lai.

Do uu tien: Trung binh, nhung rat nen lam truoc khi nop bao cao/tai lieu.

## 4. Danh sach viec nen lam tiep theo

Neu muon hoan thien dep de nop/demo, nen lam theo thu tu:

1. Sua encoding tieng Viet trong tai lieu va message neu team can hien thi tieng Viet.
2. Tich hop Course Service cua Nhom 1 neu da co API.
3. Lam QR attendance neu muon co diem nang cao khi demo.
4. Bo sung RabbitMQ/Event-driven neu giang vien yeu cau microservice nang cao.

## 5. Muc do hoan thanh theo task cua Thuyet

| Nhom viec | Trang thai | Ghi chu |
| --- | --- | --- |
| Enrollment API | Da hoan thanh co ban | Can tich hop Nhom 1 de validate ClassId neu co API |
| Attendance API | Da hoan thanh co ban | Da co diem danh le va bulk attendance |
| Tinh % chuyen can | Da hoan thanh | Da co endpoint summary rieng |
| Grade API | Da hoan thanh co ban | Da validate diem 0-10 |
| Tinh diem trung binh | Da co | Dang nam trong Result API |
| Unit Test | Da co | 12/12 tests pass |
| QR attendance | Chua lam | Task nang cao |
| RabbitMQ/Event-driven | Chua lam | Task tuong lai |
| HTTP Client Nhom 1 | Chua lam | Phu thuoc API cua Nhom 1 |

## 6. Ket luan bao cao cho nhom

Co the noi:

"Phan Business Logic API cua Thuyet da co cac API chinh cho dang ky lop, diem danh le/hang loat, nhap diem, tinh ty le chuyen can va tinh ket qua hoc tap. Code da build thanh cong va unit test pass 12/12. Cac phan con lai chu yeu la nang cao/tich hop: QR attendance, Course Service cua Nhom 1, RabbitMQ va chuan hoa encoding tai lieu."
