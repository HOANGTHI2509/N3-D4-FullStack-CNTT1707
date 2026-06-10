using SmartCampus.BLL.DTOs.Grades;
using SmartCampus.BLL.Services;

namespace SmartCampus.BLL.Services;

public interface IGradeService
{
    Task<ServiceResult<bool>> SubmitBulkGradesAsync(SubmitGradeRequest request);
}
