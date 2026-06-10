using SmartCampus.BLL.DTOs.Attendances;
using SmartCampus.BLL.Services;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;
using Xunit;

namespace SmartCampus.BLL.Tests;

public class AttendanceServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsConflict_WhenStudentIsNotEnrolled()
    {
        var repository = new FakeAttendanceRepository
        {
            Student = CreateStudent(),
            HasActiveEnrollment = false
        };
        var service = new AttendanceService(repository);

        var result = await service.CreateAsync(new CreateAttendanceRequest
        {
            StudentId = 1,
            ClassId = 10,
            AttendanceDate = new DateOnly(2026, 6, 10),
            Status = "Present"
        });

        Assert.Equal(ServiceResultType.Conflict, result.Type);
        Assert.Empty(repository.Attendances);
    }

    [Fact]
    public async Task CreateBulkAsync_CreatesAllAttendances_WhenRequestIsValid()
    {
        var repository = new FakeAttendanceRepository
        {
            Student = CreateStudent(),
            HasActiveEnrollment = true
        };
        var service = new AttendanceService(repository);

        var result = await service.CreateBulkAsync(new CreateBulkAttendanceRequest
        {
            ClassId = 10,
            AttendanceDate = new DateOnly(2026, 6, 10),
            Students =
            [
                new BulkAttendanceItemRequest { StudentId = 1, Status = "Present" },
                new BulkAttendanceItemRequest { StudentId = 2, Status = "Absent" }
            ]
        });

        Assert.Equal(ServiceResultType.Success, result.Type);
        Assert.Equal(2, result.Data!.CreatedCount);
        Assert.Equal(2, repository.Attendances.Count);
    }

    [Fact]
    public async Task GetSummaryAsync_ReturnsAttendancePercentage()
    {
        var repository = new FakeAttendanceRepository
        {
            Student = CreateStudent(),
            HasActiveEnrollment = true
        };
        repository.Attendances.AddRange(
        [
            CreateAttendance("Present"),
            CreateAttendance("Late"),
            CreateAttendance("Absent")
        ]);
        var service = new AttendanceService(repository);

        var result = await service.GetSummaryAsync(1, 10);

        Assert.Equal(ServiceResultType.Success, result.Type);
        Assert.Equal(3, result.Data!.TotalSessions);
        Assert.Equal(2, result.Data.AttendedSessions);
        Assert.Equal(66.67m, result.Data.AttendancePercentage);
    }

    private static Student CreateStudent()
    {
        return new Student
        {
            Id = 1,
            StudentCode = "SV001",
            FullName = "Nguyen Van A",
            DateOfBirth = new DateOnly(2000, 1, 1)
        };
    }

    private static Attendance CreateAttendance(string status)
    {
        return new Attendance
        {
            StudentId = 1,
            ClassId = 10,
            AttendanceDate = new DateOnly(2026, 6, 10),
            Status = status
        };
    }

    private sealed class FakeAttendanceRepository : IAttendanceRepository
    {
        public Student? Student { get; init; }

        public bool HasActiveEnrollment { get; init; }

        public List<Attendance> Attendances { get; } = [];

        public Task<Student?> GetStudentByIdAsync(int studentId)
        {
            if (Student is null)
            {
                return Task.FromResult<Student?>(null);
            }

            return Task.FromResult<Student?>(new Student
            {
                Id = studentId,
                StudentCode = Student.StudentCode,
                FullName = Student.FullName,
                DateOfBirth = Student.DateOfBirth
            });
        }

        public Task<bool> HasActiveEnrollmentAsync(int studentId, int classId)
        {
            return Task.FromResult(HasActiveEnrollment);
        }

        public Task<Attendance?> GetByIdAsync(int attendanceId)
        {
            return Task.FromResult(Attendances.FirstOrDefault(attendance => attendance.Id == attendanceId));
        }

        public Task<Attendance?> GetByStudentClassAndDateAsync(
            int studentId,
            int classId,
            DateOnly attendanceDate)
        {
            return Task.FromResult(Attendances.FirstOrDefault(attendance =>
                attendance.StudentId == studentId
                && attendance.ClassId == classId
                && attendance.AttendanceDate == attendanceDate));
        }

        public Task<IReadOnlyList<Attendance>> GetByClassIdAsync(int classId)
        {
            return Task.FromResult<IReadOnlyList<Attendance>>(
                Attendances.Where(attendance => attendance.ClassId == classId).ToList());
        }

        public Task<IReadOnlyList<Attendance>> GetByStudentAndClassAsync(int studentId, int classId)
        {
            return Task.FromResult<IReadOnlyList<Attendance>>(
                Attendances
                    .Where(attendance => attendance.StudentId == studentId && attendance.ClassId == classId)
                    .ToList());
        }

        public Task AddAsync(Attendance attendance)
        {
            attendance.Id = Attendances.Count + 1;
            Attendances.Add(attendance);
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IEnumerable<Attendance> attendances)
        {
            foreach (var attendance in attendances)
            {
                attendance.Id = Attendances.Count + 1;
                Attendances.Add(attendance);
            }

            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
