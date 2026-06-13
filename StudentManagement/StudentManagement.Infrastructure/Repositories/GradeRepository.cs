using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public GradeRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Grade grade)
    {
        await _context.Grades.AddAsync(grade);
    }

    public Task<Grade?> GetByIdAsync(int id)
    {
        return _context.Grades.FirstOrDefaultAsync(g => g.Id == id);
    }

    public void Update(Grade grade)
    {
        _context.Grades.Update(grade);
    }

    public async Task AddRangeAsync(IEnumerable<Grade> grades)
    {
        await _context.Grades.AddRangeAsync(grades);
    }

    public Task<List<Grade>> GetByClassAsync(int classId)
    {
        return _context.Grades.Where(g => g.ClassId == classId).ToListAsync();
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

