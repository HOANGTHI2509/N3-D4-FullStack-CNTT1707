using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly N3D4StudentMgmtContext _context;

    public EnrollmentRepository(N3D4StudentMgmtContext context)
    {
        _context = context;
    }

    public Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(student => student.Id == studentId);
    }

    public Task<Enrollment?> GetByIdAsync(int enrollmentId)
    {
        return _context.Enrollments
            .FirstOrDefaultAsync(enrollment => enrollment.Id == enrollmentId);
    }

    public Task<Enrollment?> GetByStudentAndClassAsync(int studentId, int classId)
    {
        return _context.Enrollments
            .FirstOrDefaultAsync(enrollment =>
                enrollment.StudentId == studentId && enrollment.ClassId == classId);
    }

    public async Task<IReadOnlyList<Enrollment>> GetByClassIdAsync(int classId)
    {
        return await _context.Enrollments
            .AsNoTracking()
            .Include(enrollment => enrollment.Student)
            .Where(enrollment => enrollment.ClassId == classId)
            .OrderBy(enrollment => enrollment.Student.StudentCode)
            .ToListAsync();
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    public void Update(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

