using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.DTOs.Enrollments;
using StudentManagement.BLL.Services;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/v1/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<EnrollmentResponse>>> Create(
        CreateEnrollmentRequest request)
    {
        var result = await _enrollmentService.CreateAsync(request);

        return result.Type switch
        {
            ServiceResultType.Success => StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<EnrollmentResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<EnrollmentResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<EnrollmentResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPatch("{id:int}/cancel")]
    public async Task<ActionResult<ApiResponse<EnrollmentResponse>>> Cancel(int id)
    {
        var result = await _enrollmentService.CancelAsync(id);

        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<EnrollmentResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<EnrollmentResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<EnrollmentResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet("classes/{classId:int}/students")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ClassStudentResponse>>>> GetClassStudents(
        int classId)
    {
        var students = await _enrollmentService.GetStudentsByClassIdAsync(classId);

        return Ok(ApiResponse<IReadOnlyList<ClassStudentResponse>>.Ok(
            students,
            "Lấy danh sách học viên của lớp thành công."));
    }

}

