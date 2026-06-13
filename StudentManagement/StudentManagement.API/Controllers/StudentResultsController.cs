using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Models;
using StudentManagement.BLL.DTOs.Results;
using StudentManagement.BLL.Services;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/v1/results")]
public class StudentResultsController : ControllerBase
{
    private readonly IStudentResultService _studentResultService;

    public StudentResultsController(IStudentResultService studentResultService)
    {
        _studentResultService = studentResultService;
    }

    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<StudentResultResponse>>> GetStudentResult(int studentId, int classId)
    {
        try
        {
            var result = await _studentResultService.GetStudentResultAsync(studentId, classId);
            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<StudentResultResponse>.Ok(result.Data!, result.Message)),
                ServiceResultType.NotFound => NotFound(ApiResponse<StudentResultResponse>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResultResponse>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResultResponse>.Fail(ex.Message));
        }
    }
}

