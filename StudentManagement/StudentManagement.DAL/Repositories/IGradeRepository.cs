using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories;

public interface IGradeRepository
{
    Task AddRangeAsync(IEnumerable<Grade> grades);
    Task<List<Grade>> GetByStudentAndClassAsync(int studentId, int classId);
    void RemoveRange(IEnumerable<Grade> grades);
    Task SaveChangesAsync();
}

