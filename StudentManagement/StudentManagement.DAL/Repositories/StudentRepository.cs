using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public StudentRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Student>> GetPagedAsync(int page, int pageSize, string? searchTerm)
    {
        var query = _context.Students.AsNoTracking().Where(s => !s.IsDeleted);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s => s.FullName.Contains(searchTerm) || s.StudentCode.Contains(searchTerm));
        }

        return await query
            .OrderByDescending(s => s.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync(string? searchTerm)
    {
        var query = _context.Students.Where(s => !s.IsDeleted);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s => s.FullName.Contains(searchTerm) || s.StudentCode.Contains(searchTerm));
        }

        return await query.CountAsync();
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



    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

