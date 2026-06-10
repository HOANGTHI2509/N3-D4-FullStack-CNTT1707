using Microsoft.AspNetCore.Mvc;
using SmartCampus.BLL.DTOs.Attendances;
using SmartCampus.BLL.Services;
using SmartCampus.Models;

namespace SmartCampus.Controllers;

[ApiController]
[Route("api/v1/attendance")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendancesController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    /// <summary>
    /// Gửi danh sách điểm danh cho một buổi học và tự động tính lại % chuyên cần
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> SubmitAttendance([FromBody] SubmitAttendanceRequest request)
    {
        try
        {
            var result = await _attendanceService.SubmitAttendanceAsync(request);

            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<bool>.Ok(result.Data, result.Message)),
                ServiceResultType.Conflict => Conflict(ApiResponse<bool>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail(ex.Message));
        }
    }
}
