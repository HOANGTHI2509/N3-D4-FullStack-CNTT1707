using StudentManagement.BLL.DTOs.Enrollments;
using StudentManagement.BLL.Services;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories;
using Xunit;

namespace StudentManagement.BLL.Tests;

public class EnrollmentServiceTests
{
    [Fact]
    public async Task CreateAsync_CreatesEnrollment_WhenRequestIsValid()
    {
        var repository = new FakeEnrollmentRepository
        {
            Student = CreateStudent()
        };
        var service = new EnrollmentService(repository);

        var result = await service.CreateAsync(new CreateEnrollmentRequest
        {
            StudentId = 1,
            ClassId = 10
        });

        Assert.Equal(ServiceResultType.Success, result.Type);
        Assert.Equal("Đang học", result.Data!.Status);
        Assert.Single(repository.Enrollments);
    }

    [Fact]
    public async Task CreateAsync_ReturnsNotFound_WhenStudentDoesNotExist()
    {
        var repository = new FakeEnrollmentRepository();
        var service = new EnrollmentService(repository);

        var result = await service.CreateAsync(new CreateEnrollmentRequest
        {
            StudentId = 1,
            ClassId = 10
        });

        Assert.Equal(ServiceResultType.NotFound, result.Type);
        Assert.Empty(repository.Enrollments);
    }

    [Fact]
    public async Task CreateAsync_ReturnsConflict_WhenEnrollmentAlreadyExists()
    {
        var repository = new FakeEnrollmentRepository
        {
            Student = CreateStudent()
        };
        repository.Enrollments.Add(new Enrollment
        {
            Id = 1,
            StudentId = 1,
            ClassId = 10,
            Status = "Đang học"
        });
        var service = new EnrollmentService(repository);

        var result = await service.CreateAsync(new CreateEnrollmentRequest
        {
            StudentId = 1,
            ClassId = 10
        });

        Assert.Equal(ServiceResultType.Conflict, result.Type);
        Assert.Single(repository.Enrollments);
    }

    [Fact]
    public async Task CancelAsync_ChangesStatus_WhenEnrollmentExists()
    {
        var enrollment = new Enrollment
        {
            Id = 1,
            StudentId = 1,
            ClassId = 10,
            Status = "Đang học"
        };
        var repository = new FakeEnrollmentRepository();
        repository.Enrollments.Add(enrollment);
        var service = new EnrollmentService(repository);

        var result = await service.CancelAsync(1);

        Assert.Equal(ServiceResultType.Success, result.Type);
        Assert.Equal("Đã hủy", enrollment.Status);
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

    private sealed class FakeEnrollmentRepository : IEnrollmentRepository
    {
        public Student? Student { get; init; }

        public List<Enrollment> Enrollments { get; } = [];

        public Task<Student?> GetStudentByIdAsync(int studentId)
        {
            return Task.FromResult(Student?.Id == studentId ? Student : null);
        }

        public Task<Enrollment?> GetByIdAsync(int enrollmentId)
        {
            return Task.FromResult(Enrollments.FirstOrDefault(enrollment => enrollment.Id == enrollmentId));
        }

        public Task<Enrollment?> GetByStudentAndClassAsync(int studentId, int classId)
        {
            return Task.FromResult(Enrollments.FirstOrDefault(enrollment =>
                enrollment.StudentId == studentId && enrollment.ClassId == classId));
        }

        public Task<IReadOnlyList<Enrollment>> GetByClassIdAsync(int classId)
        {
            IReadOnlyList<Enrollment> result = Enrollments
                .Where(enrollment => enrollment.ClassId == classId)
                .ToList();

            return Task.FromResult(result);
        }

        public Task AddAsync(Enrollment enrollment)
        {
            enrollment.Id = Enrollments.Count + 1;
            Enrollments.Add(enrollment);
            return Task.CompletedTask;
        }

        public void Update(Enrollment enrollment)
        {
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}

