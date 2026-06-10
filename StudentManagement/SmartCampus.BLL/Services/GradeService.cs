using SmartCampus.BLL.DTOs.Grades;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;

namespace SmartCampus.BLL.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GradeService(IGradeRepository gradeRepository, IEnrollmentRepository enrollmentRepository)
    {
        _gradeRepository = gradeRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<ServiceResult<bool>> SubmitBulkGradesAsync(SubmitGradeRequest request)
    {
        if (request.Records == null || !request.Records.Any())
        {
            return ServiceResult<bool>.Conflict("Danh sách điểm trống.");
        }

        var newGrades = new List<Grade>();

        foreach (var record in request.Records)
        {
            // 1. Xóa điểm cũ nếu có
            var existingGrades = await _gradeRepository.GetByStudentAndClassAsync(record.StudentId, request.ClassId);
            if (existingGrades.Any())
            {
                _gradeRepository.RemoveRange(existingGrades);
            }

            // 2. Thêm điểm mới
            newGrades.Add(new Grade
            {
                StudentId = record.StudentId,
                ClassId = request.ClassId,
                GradeType = "MidTerm",
                Score = record.MidTermScore
            });

            newGrades.Add(new Grade
            {
                StudentId = record.StudentId,
                ClassId = request.ClassId,
                GradeType = "FinalTerm",
                Score = record.FinalTermScore
            });

            // 3. Tính điểm tổng kết
            decimal total = (record.MidTermScore * 0.3m) + (record.FinalTermScore * 0.7m);
            string finalResult = (total >= 5.0m && record.FinalTermScore >= 4.0m) ? "Passed" : "Failed";

            // 4. Cập nhật Enrollment
            var enrollment = await _enrollmentRepository.GetByStudentAndClassAsync(record.StudentId, request.ClassId);
            if (enrollment != null)
            {
                enrollment.FinalResult = finalResult;
                _enrollmentRepository.Update(enrollment);
            }
        }

        // Lưu danh sách điểm mới
        await _gradeRepository.AddRangeAsync(newGrades);

        // Lưu tất cả thay đổi (cả Grade và Enrollment)
        await _gradeRepository.SaveChangesAsync();
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<bool>.Success(true, "Nhập điểm và tính kết quả thành công.");
    }
}
