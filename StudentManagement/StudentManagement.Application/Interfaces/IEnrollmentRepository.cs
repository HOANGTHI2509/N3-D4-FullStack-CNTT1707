using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces;

public interface IEnrollmentRepository
{
    Task<Student?> GetStudentByIdAsync(int studentId);

    Task<Enrollment?> GetByIdAsync(int enrollmentId);

    Task<Enrollment?> GetByStudentAndClassAsync(int studentId, int classId);

    Task<IReadOnlyList<Enrollment>> GetByClassIdAsync(int classId);

    Task AddAsync(Enrollment enrollment);
    
    void Update(Enrollment enrollment);

    Task SaveChangesAsync();
}

