-- Khởi tạo lại Database cho đúng Bounded Context của Nhóm 2 (Student & Attendance Service)
USE master;
GO
-- Tắt các kết nối đang mở để xóa DB cũ (nếu chạy lại)
IF EXISTS(select * from sys.databases where name='N3_D4_StudentMgmt')
BEGIN
    ALTER DATABASE N3_D4_StudentMgmt SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE N3_D4_StudentMgmt;
END
GO

CREATE DATABASE N3_D4_StudentMgmt;
GO
USE N3_D4_StudentMgmt;
GO

-- 1. Bảng Students (Hồ sơ học viên)
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT UNIQUE,                         -- Lưu External ID từ Service của Nhóm 3 (Payment & Report)
    StudentCode VARCHAR(20) UNIQUE NOT NULL,
    IdentityCardNumber VARCHAR(20) UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    Email VARCHAR(100) UNIQUE,
    PhoneNumber VARCHAR(15),
    Address NVARCHAR(255),
    AvatarUrl NVARCHAR(MAX),
    EnrollmentDate DATE DEFAULT GETDATE(),
    Major NVARCHAR(100),
    Status NVARCHAR(50) DEFAULT N'Đang học',
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 2. Bảng Enrollments (Học viên đăng ký học lớp nào)
CREATE TABLE Enrollments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,                      -- Lưu External ID từ Service của Nhóm 1 (Course & Schedule)
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT N'Đang học',
    
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT UC_Student_Class UNIQUE(StudentId, ClassId) 
);
GO

-- 3. Bảng Attendances (Điểm danh học viên theo lớp)
CREATE TABLE Attendances (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,                      -- Lưu External ID từ Service của Nhóm 1
    AttendanceDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Note NVARCHAR(255),
    
    FOREIGN KEY (StudentId) REFERENCES Students(Id)
);
GO

-- 4. Bảng Grades (Điểm số học viên theo lớp)
CREATE TABLE Grades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,                      -- Lưu External ID từ Service của Nhóm 1
    GradeType NVARCHAR(50) NOT NULL,
    Score DECIMAL(5,2) CHECK (Score >= 0 AND Score <= 10),
    Note NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    
    FOREIGN KEY (StudentId) REFERENCES Students(Id)
);
GO
