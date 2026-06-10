using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public interface IGradeRepository
{
    Task<Student?> GetStudentByIdAsync(int studentId);

    Task<bool> HasActiveEnrollmentAsync(int studentId, int classId);

    Task<Grade?> GetByIdAsync(int gradeId);

    Task<IReadOnlyList<Grade>> GetByClassIdAsync(int classId);

    Task<IReadOnlyList<Grade>> GetByStudentAndClassAsync(int studentId, int classId);

    Task AddAsync(Grade grade);

    Task SaveChangesAsync();
}
