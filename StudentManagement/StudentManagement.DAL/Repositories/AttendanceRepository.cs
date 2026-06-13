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

    public async Task AddAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
    }

    public Task<Attendance?> GetByIdAsync(int id)
    {
        return _context.Attendances.FirstOrDefaultAsync(a => a.Id == id);
    }

    public void Update(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
    }

    public async Task AddRangeAsync(IEnumerable<Attendance> attendances)
    {
        await _context.Attendances.AddRangeAsync(attendances);
    }

    public Task<List<Attendance>> GetByClassAsync(int classId)
    {
        return _context.Attendances.Where(a => a.ClassId == classId).ToListAsync();
    }

    public Task<List<Attendance>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        return _context.Attendances.Where(a => a.ClassId == classId && a.StudentId == studentId).ToListAsync();
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

