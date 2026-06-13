using StudentManagement.BLL.DTOs.Results;
using StudentManagement.DAL.Repositories;

namespace StudentManagement.BLL.Services;

public class StudentResultService : IStudentResultService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IGradeRepository _gradeRepository;

    public StudentResultService(IEnrollmentRepository enrollmentRepository, IGradeRepository gradeRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<ServiceResult<StudentResultResponse>> GetStudentResultAsync(int studentId, int classId)
    {
        var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
        if (student == null) return ServiceResult<StudentResultResponse>.NotFound("Không tìm thấy học viên.");

        var enrollment = await _enrollmentRepository.GetByStudentAndClassAsync(studentId, classId);
        if (enrollment == null) return ServiceResult<StudentResultResponse>.NotFound("Học viên chưa đăng ký lớp này.");

        var grades = await _gradeRepository.GetByStudentAndClassAsync(studentId, classId);
        
        var midterm = grades.FirstOrDefault(g => g.GradeType == "MidTerm")?.Score ?? 0;
        var final = grades.FirstOrDefault(g => g.GradeType == "FinalTerm")?.Score ?? 0;
        var attendance = enrollment.AttendancePercentage ?? 0;
        var average = (midterm * 0.3m) + (final * 0.7m);

        // If the result hasn't been set in the database, calculate it dynamically based on the same rules.
        string finalResult = enrollment.FinalResult;
        if (string.IsNullOrEmpty(finalResult))
        {
             finalResult = (average >= 5.0m && final >= 4.0m && attendance >= 70) ? "Passed" : "Failed";
        }

        var response = new StudentResultResponse
        {
            StudentId = studentId,
            ClassId = classId,
            StudentName = student.FullName,
            AttendancePercentage = attendance,
            MidtermScore = midterm,
            FinalScore = final,
            AverageScore = average,
            Result = finalResult
        };

        return ServiceResult<StudentResultResponse>.Success(response, "Lấy kết quả thành công.");
    }
}
