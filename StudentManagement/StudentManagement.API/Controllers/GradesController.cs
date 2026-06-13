using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Grades;
using StudentManagement.Application.Services;
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
    public async Task<ActionResult<ApiResponse<bool>>> CreateGrade([FromBody] CreateGradeRequest request)
    {
        try
        {
            var result = await _gradeService.CreateGradeAsync(request);
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
    public async Task<ActionResult<ApiResponse<bool>>> UpdateGrade(int id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var result = await _gradeService.UpdateGradeAsync(id, request);
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
    public async Task<ActionResult<ApiResponse<List<StudentGradeResponse>>>> GetGradesByClass(int classId)
    {
        try
        {
            var result = await _gradeService.GetGradesByClassAsync(classId);
            return Ok(ApiResponse<List<StudentGradeResponse>>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<List<StudentGradeResponse>>.Fail(ex.Message));
        }
    }

    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<StudentGradeResponse>>> GetGradesByStudentAndClass(int studentId, int classId)
    {
        try
        {
            var result = await _gradeService.GetStudentGradeAsync(studentId, classId);
            return Ok(ApiResponse<StudentGradeResponse>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentGradeResponse>.Fail(ex.Message));
        }
    }
}

