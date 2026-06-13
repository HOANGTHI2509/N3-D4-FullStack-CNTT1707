using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.DTOs.Attendances;
using StudentManagement.BLL.Services;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

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
    /// Tạo mới một điểm danh
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> CreateAttendance([FromBody] object request)
    {
        return Ok(ApiResponse<bool>.Ok(true, "Mock data"));
    }

    /// <summary>
    /// Gửi danh sách điểm danh cho một buổi học và tự động tính lại % chuyên cần
    /// </summary>
    [HttpPost("bulk")]
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateAttendance(int id, [FromBody] object request)
    {
        return Ok(ApiResponse<bool>.Ok(true, "Mock data"));
    }

    [HttpGet("classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetAttendancesByClass(int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }

    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetAttendancesByStudentAndClass(int studentId, int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }

    [HttpGet("summary/students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetAttendanceSummary(int studentId, int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }
}

