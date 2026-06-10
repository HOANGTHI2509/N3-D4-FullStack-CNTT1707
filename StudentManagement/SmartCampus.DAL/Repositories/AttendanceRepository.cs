using Microsoft.EntityFrameworkCore;
using SmartCampus.DAL.Constants;
using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public AttendanceRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(student => student.Id == studentId);
    }

    public Task<bool> HasActiveEnrollmentAsync(int studentId, int classId)
    {
        return _context.Enrollments.AnyAsync(enrollment =>
            enrollment.StudentId == studentId
            && enrollment.ClassId == classId
            && enrollment.Status != EnrollmentStatuses.Cancelled);
    }

    public Task<Attendance?> GetByIdAsync(int attendanceId)
    {
        return _context.Attendances
            .FirstOrDefaultAsync(attendance => attendance.Id == attendanceId);
    }

    public Task<Attendance?> GetByStudentClassAndDateAsync(
        int studentId,
        int classId,
        DateOnly attendanceDate)
    {
        return _context.Attendances.FirstOrDefaultAsync(attendance =>
            attendance.StudentId == studentId
            && attendance.ClassId == classId
            && attendance.AttendanceDate == attendanceDate);
    }

    public async Task<IReadOnlyList<Attendance>> GetByClassIdAsync(int classId)
    {
        return await _context.Attendances
            .AsNoTracking()
            .Where(attendance => attendance.ClassId == classId)
            .OrderByDescending(attendance => attendance.AttendanceDate)
            .ThenBy(attendance => attendance.StudentId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Attendance>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        return await _context.Attendances
            .AsNoTracking()
            .Where(attendance => attendance.StudentId == studentId && attendance.ClassId == classId)
            .OrderBy(attendance => attendance.AttendanceDate)
            .ToListAsync();
    }

    public async Task AddAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
    }

    public async Task AddRangeAsync(IEnumerable<Attendance> attendances)
    {
        await _context.Attendances.AddRangeAsync(attendances);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
