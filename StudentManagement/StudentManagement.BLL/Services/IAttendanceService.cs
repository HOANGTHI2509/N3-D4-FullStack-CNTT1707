using StudentManagement.BLL.DTOs.Attendances;

namespace StudentManagement.BLL.Services;

public interface IAttendanceService
{
    Task<ServiceResult<bool>> SubmitAttendanceAsync(SubmitAttendanceRequest request);
}

