using Microsoft.EntityFrameworkCore;
using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public GradeRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<Grade> grades)
    {
        await _context.Grades.AddRangeAsync(grades);
    }

    public Task<List<Grade>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        return _context.Grades
            .Where(g => g.StudentId == studentId && g.ClassId == classId)
            .ToListAsync();
    }

    public void RemoveRange(IEnumerable<Grade> grades)
    {
        _context.Grades.RemoveRange(grades);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
