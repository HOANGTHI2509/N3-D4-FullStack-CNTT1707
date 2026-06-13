using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Attendances;
using StudentManagement.Application.Services;
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
    public async Task<ActionResult<ApiResponse<bool>>> CreateAttendance([FromBody] CreateAttendanceRequest request)
    {
        try
        {
            var result = await _attendanceService.CreateAttendanceAsync(request);
            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<bool>.Ok(result.Data, result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail(ex.Message));
        }
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
    public async Task<ActionResult<ApiResponse<bool>>> UpdateAttendance(int id, [FromBody] UpdateAttendanceRequest request)
    {
        try
        {
            var result = await _attendanceService.UpdateAttendanceAsync(id, request);
            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<bool>.Ok(result.Data, result.Message)),
                ServiceResultType.NotFound => NotFound(ApiResponse<bool>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<bool>.Fail(ex.Message));
        }
    }

    [HttpGet("classes/{classId}")]
    public async Task<ActionResult<ApiResponse<List<StudentAttendanceResponse>>>> GetAttendancesByClass(int classId)
    {
        try
        {
            var result = await _attendanceService.GetAttendancesByClassAsync(classId);
            return Ok(ApiResponse<List<StudentAttendanceResponse>>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<List<StudentAttendanceResponse>>.Fail(ex.Message));
        }
    }

    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<List<StudentAttendanceResponse>>>> GetAttendancesByStudentAndClass(int studentId, int classId)
    {
        try
        {
            var result = await _attendanceService.GetAttendancesByStudentAndClassAsync(studentId, classId);
            return Ok(ApiResponse<List<StudentAttendanceResponse>>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<List<StudentAttendanceResponse>>.Fail(ex.Message));
        }
    }

    [HttpGet("summary/students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<AttendanceSummaryResponse>>> GetAttendanceSummary(int studentId, int classId)
    {
        try
        {
            var result = await _attendanceService.GetAttendanceSummaryAsync(studentId, classId);
            return Ok(ApiResponse<AttendanceSummaryResponse>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<AttendanceSummaryResponse>.Fail(ex.Message));
        }
    }
}

