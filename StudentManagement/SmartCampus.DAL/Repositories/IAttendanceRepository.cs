using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public interface IAttendanceRepository
{
    Task<Student?> GetStudentByIdAsync(int studentId);

    Task<bool> HasActiveEnrollmentAsync(int studentId, int classId);

    Task<Attendance?> GetByIdAsync(int attendanceId);

    Task<Attendance?> GetByStudentClassAndDateAsync(int studentId, int classId, DateOnly attendanceDate);

    Task<IReadOnlyList<Attendance>> GetByClassIdAsync(int classId);

    Task<IReadOnlyList<Attendance>> GetByStudentAndClassAsync(int studentId, int classId);

    Task AddAsync(Attendance attendance);

    Task AddRangeAsync(IEnumerable<Attendance> attendances);

    Task SaveChangesAsync();
}
