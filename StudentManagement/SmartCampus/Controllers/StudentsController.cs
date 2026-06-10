using Microsoft.AspNetCore.Mvc;
using SmartCampus.BLL.DTOs.Students;
using SmartCampus.BLL.Services;
using SmartCampus.Models;

namespace SmartCampus.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Lấy danh sách toàn bộ học viên
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<StudentResponse>>>> GetAll()
    {
        var result = await _studentService.GetAllAsync();
        
        return Ok(ApiResponse<IReadOnlyList<StudentResponse>>.Ok(result.Data!, "Lấy danh sách học viên thành công."));
    }

    /// <summary>
    /// Lấy thông tin học viên theo ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentResponse>>> GetById(int id)
    {
        var result = await _studentService.GetByIdAsync(id);

        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<StudentResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<StudentResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    /// <summary>
    /// Thêm một học viên mới vào hệ thống
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<StudentResponse>>> Create([FromBody] CreateStudentRequest request)
    {
        var result = await _studentService.CreateAsync(request);

        return result.Type switch
        {
            ServiceResultType.Success => StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<StudentResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<StudentResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
