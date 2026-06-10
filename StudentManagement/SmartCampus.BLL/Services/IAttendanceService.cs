using SmartCampus.BLL.DTOs.Attendances;

namespace SmartCampus.BLL.Services;

public interface IAttendanceService
{
    Task<ServiceResult<bool>> SubmitAttendanceAsync(SubmitAttendanceRequest request);
}
