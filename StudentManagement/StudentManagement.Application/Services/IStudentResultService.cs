using StudentManagement.BLL.DTOs.Results;

namespace StudentManagement.BLL.Services;

public interface IStudentResultService
{
    Task<ServiceResult<StudentResultResponse>> GetStudentResultAsync(int studentId, int classId);
}
