using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public interface IAttendanceRepository
{
    Task AddRangeAsync(IEnumerable<Attendance> attendances);
    Task<int> GetTotalSessionsAsync(int classId, int studentId);
    Task<int> GetPresentSessionsAsync(int classId, int studentId);
    Task SaveChangesAsync();
}
