CREATE DATABASE N3_D4_StudentMgmt;
GO
USE N3_D4_StudentMgmt;
GO

-- 1. Bảng Accounts
CREATE TABLE Accounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'Student',
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 2. Bảng Students
CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT UNIQUE,
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
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Students_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);
GO

-- 3. Bảng Courses
CREATE TABLE Courses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CourseCode VARCHAR(20) UNIQUE NOT NULL,
    CourseName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX),
    Credits INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 4. Bảng Classes
CREATE TABLE Classes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CourseId INT NOT NULL,
    ClassName NVARCHAR(100) NOT NULL,
    StartDate DATE,
    EndDate DATE,
    MaxStudents INT DEFAULT 40,
    Status NVARCHAR(50) DEFAULT N'Đang mở',
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);
GO

-- 5. Bảng Enrollments
CREATE TABLE Enrollments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT N'Đang học',
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (ClassId) REFERENCES Classes(Id),
    CONSTRAINT UC_Student_Class UNIQUE(StudentId, ClassId) 
);
GO

-- 6. Bảng Attendances
CREATE TABLE Attendances (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Note NVARCHAR(255),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (ClassId) REFERENCES Classes(Id)
);
GO

-- 7. Bảng Grades
CREATE TABLE Grades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    GradeType NVARCHAR(50) NOT NULL,
    Score DECIMAL(5,2) CHECK (Score >= 0 AND Score <= 10),
    Note NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (ClassId) REFERENCES Classes(Id)
);
GO
