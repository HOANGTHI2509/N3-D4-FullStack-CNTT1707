using Microsoft.EntityFrameworkCore;
using SmartCampus.DAL.Constants;
using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public GradeRepository(N3D4StudentMgmtContext context)
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

    public Task<Grade?> GetByIdAsync(int gradeId)
    {
        return _context.Grades
            .FirstOrDefaultAsync(grade => grade.Id == gradeId);
    }

    public async Task<IReadOnlyList<Grade>> GetByClassIdAsync(int classId)
    {
        return await _context.Grades
            .AsNoTracking()
            .Where(grade => grade.ClassId == classId)
            .OrderBy(grade => grade.StudentId)
            .ThenBy(grade => grade.GradeType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Grade>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        return await _context.Grades
            .AsNoTracking()
            .Where(grade => grade.StudentId == studentId && grade.ClassId == classId)
            .OrderBy(grade => grade.GradeType)
            .ToListAsync();
    }

    public async Task AddAsync(Grade grade)
    {
        await _context.Grades.AddAsync(grade);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
