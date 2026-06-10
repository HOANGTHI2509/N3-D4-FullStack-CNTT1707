using SmartCampus.BLL.DTOs.Grades;

namespace SmartCampus.BLL.Services;

public interface IGradeService
{
    Task<ServiceResult<GradeResponse>> CreateAsync(CreateGradeRequest request);

    Task<ServiceResult<GradeResponse>> UpdateAsync(int gradeId, UpdateGradeRequest request);

    Task<IReadOnlyList<GradeResponse>> GetByClassIdAsync(int classId);

    Task<IReadOnlyList<GradeResponse>> GetByStudentAndClassAsync(int studentId, int classId);
}
