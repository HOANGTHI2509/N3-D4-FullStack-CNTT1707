using StudentManagement.BLL.DTOs.Grades;
using StudentManagement.BLL.Services;

namespace StudentManagement.BLL.Services;

public interface IGradeService
{
    Task<ServiceResult<bool>> SubmitBulkGradesAsync(SubmitGradeRequest request);
}

