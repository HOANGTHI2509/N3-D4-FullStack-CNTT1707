using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public interface IStudentRepository
{
    Task<IReadOnlyList<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByStudentCodeAsync(string studentCode);
    Task<Student?> GetByIdentityCardAsync(string idCard);
    Task AddAsync(Student student);
    void Update(Student student);
    void Delete(Student student);
    Task SaveChangesAsync();
}
