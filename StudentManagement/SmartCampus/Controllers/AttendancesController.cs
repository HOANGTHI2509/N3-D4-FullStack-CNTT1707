using Microsoft.AspNetCore.Mvc;
using SmartCampus.BLL.DTOs.Attendances;
using SmartCampus.BLL.Services;
using SmartCampus.Models;

namespace SmartCampus.Controllers;

[ApiController]
[Route("api/v1/attendances")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendancesController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AttendanceResponse>>> Create(
        CreateAttendanceRequest request)
    {
        var result = await _attendanceService.CreateAsync(request);

        return ToActionResult(result);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<ApiResponse<BulkAttendanceResponse>>> CreateBulk(
        CreateBulkAttendanceRequest request)
    {
        var result = await _attendanceService.CreateBulkAsync(request);

        return result.Type switch
        {
            ServiceResultType.Success => StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<BulkAttendanceResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.BadRequest => BadRequest(ApiResponse<BulkAttendanceResponse>.Fail(result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<BulkAttendanceResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<BulkAttendanceResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<AttendanceResponse>>> Update(
        int id,
        UpdateAttendanceRequest request)
    {
        var result = await _attendanceService.UpdateAsync(id, request);

        return ToActionResult(result);
    }

    [HttpGet("classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AttendanceResponse>>>> GetClassAttendances(
        int classId)
    {
        var attendances = await _attendanceService.GetByClassIdAsync(classId);

        return Ok(ApiResponse<IReadOnlyList<AttendanceResponse>>.Ok(
            attendances,
            "Lay danh sach diem danh thanh cong."));
    }

    [HttpGet("students/{studentId:int}/classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AttendanceResponse>>>> GetStudentAttendances(
        int studentId,
        int classId)
    {
        var attendances = await _attendanceService.GetByStudentAndClassAsync(studentId, classId);

        return Ok(ApiResponse<IReadOnlyList<AttendanceResponse>>.Ok(
            attendances,
            "Lay lich su diem danh thanh cong."));
    }

    [HttpGet("summary/students/{studentId:int}/classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<AttendanceSummaryResponse>>> GetSummary(
        int studentId,
        int classId)
    {
        var result = await _attendanceService.GetSummaryAsync(studentId, classId);

        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<AttendanceSummaryResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.BadRequest => BadRequest(ApiResponse<AttendanceSummaryResponse>.Fail(result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<AttendanceSummaryResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<AttendanceSummaryResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    private ActionResult<ApiResponse<AttendanceResponse>> ToActionResult(
        ServiceResult<AttendanceResponse> result)
    {
        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<AttendanceResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.BadRequest => BadRequest(ApiResponse<AttendanceResponse>.Fail(result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<AttendanceResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<AttendanceResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
