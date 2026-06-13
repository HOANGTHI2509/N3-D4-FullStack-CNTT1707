using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public AttendanceRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<Attendance> attendances)
    {
        await _context.Attendances.AddRangeAsync(attendances);
    }

    public Task<int> GetTotalSessionsAsync(int classId, int studentId)
    {
        return _context.Attendances
            .Where(a => a.ClassId == classId && a.StudentId == studentId)
            .CountAsync();
    }

    public Task<int> GetPresentSessionsAsync(int classId, int studentId)
    {
        return _context.Attendances
            .Where(a => a.ClassId == classId && a.StudentId == studentId && a.Status == "Present")
            .CountAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

