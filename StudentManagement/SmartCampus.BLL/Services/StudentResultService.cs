using SmartCampus.BLL.DTOs.Results;
using SmartCampus.DAL.Repositories;

namespace SmartCampus.BLL.Services;

public class StudentResultService : IStudentResultService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IGradeRepository _gradeRepository;

    public StudentResultService(
        IAttendanceRepository attendanceRepository,
        IGradeRepository gradeRepository)
    {
        _attendanceRepository = attendanceRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<ServiceResult<StudentResultResponse>> GetStudentResultAsync(int studentId, int classId)
    {
        var student = await _attendanceRepository.GetStudentByIdAsync(studentId);
        if (student is null)
        {
            return ServiceResult<StudentResultResponse>.NotFound("Khong tim thay hoc vien.");
        }

        var hasEnrollment = await _attendanceRepository.HasActiveEnrollmentAsync(studentId, classId);
        if (!hasEnrollment)
        {
            return ServiceResult<StudentResultResponse>.Conflict("Hoc vien chua dang ky lop nay.");
        }

        var attendances = await _attendanceRepository.GetByStudentAndClassAsync(studentId, classId);
        var grades = await _gradeRepository.GetByStudentAndClassAsync(studentId, classId);
        var result = AcademicResultCalculator.Calculate(studentId, classId, attendances, grades);

        return ServiceResult<StudentResultResponse>.Success(
            result,
            "Tinh ket qua hoc tap thanh cong.");
    }
}
