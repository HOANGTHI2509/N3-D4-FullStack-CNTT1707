using SmartCampus.BLL.DTOs.Students;

namespace SmartCampus.BLL.Services;

public interface IStudentService
{
    Task<ServiceResult<IReadOnlyList<StudentResponse>>> GetAllAsync();
    Task<ServiceResult<StudentResponse>> GetByIdAsync(int id);
    Task<ServiceResult<StudentResponse>> CreateAsync(CreateStudentRequest request);
    // Có thể bổ sung Update, Delete tương tự
}
