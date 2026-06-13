using StudentManagement.BLL.DTOs.Grades;
using StudentManagement.BLL.Services;

namespace StudentManagement.BLL.Services;

public interface IGradeService
{
    Task<ServiceResult<bool>> SubmitBulkGradesAsync(SubmitGradeRequest request);
    Task<ServiceResult<bool>> CreateGradeAsync(CreateGradeRequest request);
    Task<ServiceResult<bool>> UpdateGradeAsync(int id, UpdateGradeRequest request);
    Task<ServiceResult<List<StudentGradeResponse>>> GetGradesByClassAsync(int classId);
    Task<ServiceResult<StudentGradeResponse>> GetStudentGradeAsync(int studentId, int classId);
}

