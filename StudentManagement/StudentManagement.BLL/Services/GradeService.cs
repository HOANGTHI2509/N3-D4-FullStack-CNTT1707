using StudentManagement.BLL.DTOs.Grades;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories;

namespace StudentManagement.BLL.Services;

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

    public async Task<ServiceResult<bool>> CreateGradeAsync(CreateGradeRequest request)
    {
        var existingGrades = await _gradeRepository.GetByStudentAndClassAsync(request.StudentId, request.ClassId);
        if (existingGrades.Any())
        {
            _gradeRepository.RemoveRange(existingGrades);
        }

        await _gradeRepository.AddAsync(new Grade { StudentId = request.StudentId, ClassId = request.ClassId, GradeType = "MidTerm", Score = request.MidTermScore });
        await _gradeRepository.AddAsync(new Grade { StudentId = request.StudentId, ClassId = request.ClassId, GradeType = "FinalTerm", Score = request.FinalTermScore });

        await _gradeRepository.SaveChangesAsync();
        return ServiceResult<bool>.Success(true, "Thêm điểm thành công.");
    }

    public async Task<ServiceResult<bool>> UpdateGradeAsync(int id, UpdateGradeRequest request)
    {
        var grade = await _gradeRepository.GetByIdAsync(id);
        if (grade == null) return ServiceResult<bool>.NotFound("Không tìm thấy bảng điểm.");
        
        var allGrades = await _gradeRepository.GetByStudentAndClassAsync(grade.StudentId, grade.ClassId);
        var midterm = allGrades.FirstOrDefault(g => g.GradeType == "MidTerm");
        var finalterm = allGrades.FirstOrDefault(g => g.GradeType == "FinalTerm");
        
        if (midterm != null) { midterm.Score = request.MidTermScore; midterm.UpdatedAt = DateTime.UtcNow; _gradeRepository.Update(midterm); }
        if (finalterm != null) { finalterm.Score = request.FinalTermScore; finalterm.UpdatedAt = DateTime.UtcNow; _gradeRepository.Update(finalterm); }

        await _gradeRepository.SaveChangesAsync();
        return ServiceResult<bool>.Success(true, "Cập nhật điểm thành công.");
    }

    public async Task<ServiceResult<List<StudentGradeResponse>>> GetGradesByClassAsync(int classId)
    {
        var grades = await _gradeRepository.GetByClassAsync(classId);
        var enrollments = await _enrollmentRepository.GetByClassIdAsync(classId);
        
        var grouped = grades.GroupBy(g => g.StudentId).ToList();
        var result = new List<StudentGradeResponse>();
        foreach (var group in grouped)
        {
            var studentId = group.Key;
            var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
            var midterm = group.FirstOrDefault(g => g.GradeType == "MidTerm")?.Score ?? 0;
            var final = group.FirstOrDefault(g => g.GradeType == "FinalTerm")?.Score ?? 0;
            var enrollment = enrollments.FirstOrDefault(e => e.StudentId == studentId);
            var attendance = enrollment?.AttendancePercentage ?? 0;
            
            result.Add(new StudentGradeResponse
            {
                StudentId = studentId,
                ClassId = classId,
                StudentName = student?.FullName ?? "Unknown",
                ClassName = $"Lớp {classId}",
                Scores = new GradeScores { Attendance = attendance, Midterm = midterm, Final = final, Average = (midterm * 0.3m) + (final * 0.7m) }
            });
        }
        return ServiceResult<List<StudentGradeResponse>>.Success(result, "Lấy danh sách điểm thành công.");
    }

    public async Task<ServiceResult<StudentGradeResponse>> GetStudentGradeAsync(int studentId, int classId)
    {
        var grades = await _gradeRepository.GetByStudentAndClassAsync(studentId, classId);
        var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
        var enrollment = await _enrollmentRepository.GetByStudentAndClassAsync(studentId, classId);

        var midterm = grades.FirstOrDefault(g => g.GradeType == "MidTerm")?.Score ?? 0;
        var final = grades.FirstOrDefault(g => g.GradeType == "FinalTerm")?.Score ?? 0;
        var attendance = enrollment?.AttendancePercentage ?? 0;

        var response = new StudentGradeResponse
        {
            StudentId = studentId,
            ClassId = classId,
            StudentName = student?.FullName ?? "Unknown",
            ClassName = $"Lớp {classId}",
            Scores = new GradeScores
            {
                Attendance = attendance,
                Midterm = midterm,
                Final = final,
                Average = (midterm * 0.3m) + (final * 0.7m)
            }
        };

        return ServiceResult<StudentGradeResponse>.Success(response, "Lấy điểm thành công.");
    }
}

