using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.DTOs;
using StudentManagement.BLL.DTOs.Students;
using StudentManagement.BLL.Services;
using StudentManagement.API.Models;

namespace StudentManagement.API.Controllers;

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
    /// Lấy danh sách học viên có phân trang và tìm kiếm
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResponse<StudentResponse>>>> GetPaged(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10, 
        [FromQuery] string? searchTerm = null)
    {
        try
        {
            var result = await _studentService.GetPagedAsync(page, pageSize, searchTerm);
            return Ok(ApiResponse<PagedResponse<StudentResponse>>.Ok(result.Data!, result.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<PagedResponse<StudentResponse>>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Lấy thông tin học viên theo ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentResponse>>> GetById(int id)
    {
        try
        {
            var result = await _studentService.GetByIdAsync(id);

            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<StudentResponse>.Ok(result.Data!, result.Message)),
                ServiceResultType.NotFound => NotFound(ApiResponse<StudentResponse>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Thêm một học viên mới vào hệ thống
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<StudentResponse>>> Create([FromBody] CreateStudentRequest request)
    {
        try
        {
            var result = await _studentService.CreateAsync(request);

            return result.Type switch
            {
                ServiceResultType.Success => StatusCode(
                    StatusCodes.Status201Created,
                    ApiResponse<StudentResponse>.Ok(result.Data!, result.Message)),
                ServiceResultType.Conflict => Conflict(ApiResponse<StudentResponse>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Cập nhật thông tin học viên
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<StudentResponse>>> Update(int id, [FromBody] UpdateStudentRequest request)
    {
        try
        {
            var result = await _studentService.UpdateAsync(id, request);

            return result.Type switch
            {
                ServiceResultType.Success => Ok(ApiResponse<StudentResponse>.Ok(result.Data!, result.Message)),
                ServiceResultType.NotFound => NotFound(ApiResponse<StudentResponse>.Fail(result.Message)),
                _ => StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail("Lỗi hệ thống."))
            };
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<StudentResponse>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Xóa mềm học viên (Soft Delete)
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        try
        {
            var result = await _studentService.DeleteAsync(id);

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
}

