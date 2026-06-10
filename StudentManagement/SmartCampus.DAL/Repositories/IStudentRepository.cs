using SmartCampus.DAL.Models;

namespace SmartCampus.DAL.Repositories;

public interface IStudentRepository
{
    Task<IReadOnlyList<Student>> GetPagedAsync(int page, int pageSize, string? searchTerm);
    Task<int> GetTotalCountAsync(string? searchTerm);
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByStudentCodeAsync(string studentCode);
    Task<Student?> GetByIdentityCardAsync(string idCard);
    Task AddAsync(Student student);
    void Update(Student student);
    Task SaveChangesAsync();
}
