using SmartCampus.BLL.DTOs;
using SmartCampus.BLL.DTOs.Students;

namespace SmartCampus.BLL.Services;

public interface IStudentService
{
    Task<ServiceResult<PagedResponse<StudentResponse>>> GetPagedAsync(int page, int pageSize, string? searchTerm);
    Task<ServiceResult<StudentResponse>> GetByIdAsync(int id);
    Task<ServiceResult<StudentResponse>> CreateAsync(CreateStudentRequest request);
    Task<ServiceResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
