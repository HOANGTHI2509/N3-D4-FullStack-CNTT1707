using SmartCampus.BLL.DTOs.Grades;
using SmartCampus.BLL.Services;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;
using Xunit;

namespace SmartCampus.BLL.Tests;

public class GradeServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsBadRequest_WhenScoreIsOutOfRange()
    {
        var repository = new FakeGradeRepository
        {
            Student = CreateStudent(),
            HasActiveEnrollment = true
        };
        var service = new GradeService(repository);

        var result = await service.CreateAsync(new CreateGradeRequest
        {
            StudentId = 1,
            ClassId = 10,
            GradeType = "Final",
            Score = 11
        });

        Assert.Equal(ServiceResultType.BadRequest, result.Type);
        Assert.Empty(repository.Grades);
    }

    [Fact]
    public async Task CreateAsync_CreatesGrade_WhenRequestIsValid()
    {
        var repository = new FakeGradeRepository
        {
            Student = CreateStudent(),
            HasActiveEnrollment = true
        };
        var service = new GradeService(repository);

        var result = await service.CreateAsync(new CreateGradeRequest
        {
            StudentId = 1,
            ClassId = 10,
            GradeType = "Final",
            Score = 8.5m
        });

        Assert.Equal(ServiceResultType.Success, result.Type);
        Assert.Single(repository.Grades);
        Assert.Equal(8.5m, result.Data!.Score);
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

    private sealed class FakeGradeRepository : IGradeRepository
    {
        public Student? Student { get; init; }

        public bool HasActiveEnrollment { get; init; }

        public List<Grade> Grades { get; } = [];

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

        public Task<Grade?> GetByIdAsync(int gradeId)
        {
            return Task.FromResult(Grades.FirstOrDefault(grade => grade.Id == gradeId));
        }

        public Task<IReadOnlyList<Grade>> GetByClassIdAsync(int classId)
        {
            return Task.FromResult<IReadOnlyList<Grade>>(
                Grades.Where(grade => grade.ClassId == classId).ToList());
        }

        public Task<IReadOnlyList<Grade>> GetByStudentAndClassAsync(int studentId, int classId)
        {
            return Task.FromResult<IReadOnlyList<Grade>>(
                Grades.Where(grade => grade.StudentId == studentId && grade.ClassId == classId).ToList());
        }

        public Task AddAsync(Grade grade)
        {
            grade.Id = Grades.Count + 1;
            Grades.Add(grade);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
