using Microsoft.AspNetCore.Mvc;
using SmartCampus.BLL.DTOs.Results;
using SmartCampus.BLL.Services;
using SmartCampus.Models;

namespace SmartCampus.Controllers;

[ApiController]
[Route("api/v1/results")]
public class StudentResultsController : ControllerBase
{
    private readonly IStudentResultService _studentResultService;

    public StudentResultsController(IStudentResultService studentResultService)
    {
        _studentResultService = studentResultService;
    }

    [HttpGet("students/{studentId:int}/classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<StudentResultResponse>>> GetStudentResult(
        int studentId,
        int classId)
    {
        var result = await _studentResultService.GetStudentResultAsync(studentId, classId);

        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<StudentResultResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.BadRequest => BadRequest(ApiResponse<StudentResultResponse>.Fail(result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<StudentResultResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<StudentResultResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
