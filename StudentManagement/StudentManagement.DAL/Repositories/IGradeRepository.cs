using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories;

public interface IGradeRepository
{
    Task AddAsync(Grade grade);
    Task<Grade?> GetByIdAsync(int id);
    void Update(Grade grade);
    Task AddRangeAsync(IEnumerable<Grade> grades);
    Task<List<Grade>> GetByClassAsync(int classId);
    Task<List<Grade>> GetByStudentAndClassAsync(int studentId, int classId);
    void RemoveRange(IEnumerable<Grade> grades);
    Task SaveChangesAsync();
}

