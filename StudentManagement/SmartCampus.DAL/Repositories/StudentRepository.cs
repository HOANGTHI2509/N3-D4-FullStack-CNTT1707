using Microsoft.EntityFrameworkCore;
using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public StudentRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Student>> GetAllAsync()
    {
        return await _context.Students
            .AsNoTracking()
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public Task<Student?> GetByIdAsync(int id)
    {
        return _context.Students.FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<Student?> GetByStudentCodeAsync(string studentCode)
    {
        return _context.Students.FirstOrDefaultAsync(s => s.StudentCode == studentCode);
    }

    public Task<Student?> GetByIdentityCardAsync(string idCard)
    {
        return _context.Students.FirstOrDefaultAsync(s => s.IdentityCardNumber == idCard);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public void Delete(Student student)
    {
        _context.Students.Remove(student);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
