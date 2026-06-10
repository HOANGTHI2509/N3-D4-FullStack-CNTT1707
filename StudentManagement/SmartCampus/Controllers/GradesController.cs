using Microsoft.AspNetCore.Mvc;
using SmartCampus.BLL.DTOs.Grades;
using SmartCampus.BLL.Services;
using SmartCampus.Models;

namespace SmartCampus.Controllers;

[ApiController]
[Route("api/v1/grades")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<GradeResponse>>> Create(CreateGradeRequest request)
    {
        var result = await _gradeService.CreateAsync(request);

        return ToActionResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<GradeResponse>>> Update(
        int id,
        UpdateGradeRequest request)
    {
        var result = await _gradeService.UpdateAsync(id, request);

        return ToActionResult(result);
    }

    [HttpGet("classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<GradeResponse>>>> GetClassGrades(int classId)
    {
        var grades = await _gradeService.GetByClassIdAsync(classId);

        return Ok(ApiResponse<IReadOnlyList<GradeResponse>>.Ok(
            grades,
            "Lay bang diem cua lop thanh cong."));
    }

    [HttpGet("students/{studentId:int}/classes/{classId:int}")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<GradeResponse>>>> GetStudentGrades(
        int studentId,
        int classId)
    {
        var grades = await _gradeService.GetByStudentAndClassAsync(studentId, classId);

        return Ok(ApiResponse<IReadOnlyList<GradeResponse>>.Ok(
            grades,
            "Lay bang diem cua hoc vien thanh cong."));
    }

    private ActionResult<ApiResponse<GradeResponse>> ToActionResult(ServiceResult<GradeResponse> result)
    {
        return result.Type switch
        {
            ServiceResultType.Success => Ok(ApiResponse<GradeResponse>.Ok(result.Data!, result.Message)),
            ServiceResultType.BadRequest => BadRequest(ApiResponse<GradeResponse>.Fail(result.Message)),
            ServiceResultType.NotFound => NotFound(ApiResponse<GradeResponse>.Fail(result.Message)),
            ServiceResultType.Conflict => Conflict(ApiResponse<GradeResponse>.Fail(result.Message)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
