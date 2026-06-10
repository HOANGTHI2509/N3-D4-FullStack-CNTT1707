using SmartCampus.BLL.DTOs.Grades;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;

namespace SmartCampus.BLL.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<ServiceResult<GradeResponse>> CreateAsync(CreateGradeRequest request)
    {
        var validationMessage = ValidateScore(request.Score);
        if (validationMessage is not null)
        {
            return ServiceResult<GradeResponse>.BadRequest(validationMessage);
        }

        var student = await _gradeRepository.GetStudentByIdAsync(request.StudentId);
        if (student is null)
        {
            return ServiceResult<GradeResponse>.NotFound("Khong tim thay hoc vien.");
        }

        var hasEnrollment = await _gradeRepository.HasActiveEnrollmentAsync(request.StudentId, request.ClassId);
        if (!hasEnrollment)
        {
            return ServiceResult<GradeResponse>.Conflict("Hoc vien chua dang ky lop nay.");
        }

        var grade = new Grade
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            GradeType = request.GradeType.Trim(),
            Score = request.Score,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow
        };

        await _gradeRepository.AddAsync(grade);
        await _gradeRepository.SaveChangesAsync();

        return ServiceResult<GradeResponse>.Success(
            MapGrade(grade),
            "Nhap diem thanh cong.");
    }

    public async Task<ServiceResult<GradeResponse>> UpdateAsync(int gradeId, UpdateGradeRequest request)
    {
        var validationMessage = ValidateScore(request.Score);
        if (validationMessage is not null)
        {
            return ServiceResult<GradeResponse>.BadRequest(validationMessage);
        }

        var grade = await _gradeRepository.GetByIdAsync(gradeId);
        if (grade is null)
        {
            return ServiceResult<GradeResponse>.NotFound("Khong tim thay diem.");
        }

        grade.GradeType = request.GradeType.Trim();
        grade.Score = request.Score;
        grade.Note = request.Note;
        await _gradeRepository.SaveChangesAsync();

        return ServiceResult<GradeResponse>.Success(
            MapGrade(grade),
            "Cap nhat diem thanh cong.");
    }

    public async Task<IReadOnlyList<GradeResponse>> GetByClassIdAsync(int classId)
    {
        var grades = await _gradeRepository.GetByClassIdAsync(classId);

        return grades.Select(MapGrade).ToList();
    }

    public async Task<IReadOnlyList<GradeResponse>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        var grades = await _gradeRepository.GetByStudentAndClassAsync(studentId, classId);

        return grades.Select(MapGrade).ToList();
    }

    private static string? ValidateScore(decimal score)
    {
        return score is < 0 or > 10
            ? "Diem phai nam trong khoang 0 den 10."
            : null;
    }

    private static GradeResponse MapGrade(Grade grade)
    {
        return new GradeResponse
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            ClassId = grade.ClassId,
            GradeType = grade.GradeType,
            Score = grade.Score,
            Note = grade.Note,
            CreatedAt = grade.CreatedAt
        };
    }
}
