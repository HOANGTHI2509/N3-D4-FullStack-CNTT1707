using SmartCampus.BLL.DTOs.Results;

namespace SmartCampus.BLL.Services;

public interface IStudentResultService
{
    Task<ServiceResult<StudentResultResponse>> GetStudentResultAsync(int studentId, int classId);
}
