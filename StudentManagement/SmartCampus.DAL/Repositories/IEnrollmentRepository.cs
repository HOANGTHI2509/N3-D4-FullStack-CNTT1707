using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

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
