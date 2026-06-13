using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.DTOs.Grades;
using StudentManagement.BLL.Services;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/v1/grades")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    /// <summary>
    /// Nhập điểm hàng loạt và tính kết quả Passed/Failed
    /// </summary>
    [HttpPost("bulk")]
    public async Task<ActionResult<ApiResponse<bool>>> SubmitBulkGrades([FromBody] SubmitGradeRequest request)
    {
        try
        {
            var result = await _gradeService.SubmitBulkGradesAsync(request);

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

    [HttpPost]
    public async Task<ActionResult<ApiResponse<bool>>> CreateGrade([FromBody] object request)
    {
        return Ok(ApiResponse<bool>.Ok(true, "Mock data"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateGrade(int id, [FromBody] object request)
    {
        return Ok(ApiResponse<bool>.Ok(true, "Mock data"));
    }

    [HttpGet("classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetGradesByClass(int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }

    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetGradesByStudentAndClass(int studentId, int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }
}

