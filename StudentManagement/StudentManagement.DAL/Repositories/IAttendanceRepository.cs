using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories;

public interface IAttendanceRepository
{
    Task AddAsync(Attendance attendance);
    Task<Attendance?> GetByIdAsync(int id);
    void Update(Attendance attendance);
    Task AddRangeAsync(IEnumerable<Attendance> attendances);
    Task<List<Attendance>> GetByClassAsync(int classId);
    Task<List<Attendance>> GetByStudentAndClassAsync(int studentId, int classId);
    Task<int> GetTotalSessionsAsync(int classId, int studentId);
    Task<int> GetPresentSessionsAsync(int classId, int studentId);
    Task SaveChangesAsync();
}

