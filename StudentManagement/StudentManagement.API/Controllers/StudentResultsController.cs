using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

[ApiController]
[Route("api/v1/results")]
public class StudentResultsController : ControllerBase
{
    [HttpGet("students/{studentId}/classes/{classId}")]
    public async Task<ActionResult<ApiResponse<object>>> GetStudentResult(int studentId, int classId)
    {
        return Ok(ApiResponse<object>.Ok(new { }, "Mock data"));
    }
}

