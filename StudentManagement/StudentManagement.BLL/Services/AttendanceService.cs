using StudentManagement.BLL.DTOs.Attendances;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories;

namespace StudentManagement.BLL.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository, IEnrollmentRepository enrollmentRepository)
    {
        _attendanceRepository = attendanceRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<ServiceResult<bool>> SubmitAttendanceAsync(SubmitAttendanceRequest request)
    {
        if (request.Records == null || !request.Records.Any())
        {
            return ServiceResult<bool>.Conflict("Danh sách điểm danh trống.");
        }

        var attendances = request.Records.Select(r => new Attendance
        {
            ClassId = request.ClassId,
            StudentId = r.StudentId,
            AttendanceDate = request.AttendanceDate,
            Status = r.Status,
            Note = r.Note
        }).ToList();

        // 1. Lưu thông tin điểm danh
        await _attendanceRepository.AddRangeAsync(attendances);
        await _attendanceRepository.SaveChangesAsync();

        // 2. Tính lại phần trăm chuyên cần cho từng học viên
        foreach (var record in request.Records)
        {
            var totalSessions = await _attendanceRepository.GetTotalSessionsAsync(request.ClassId, record.StudentId);
            var presentSessions = await _attendanceRepository.GetPresentSessionsAsync(request.ClassId, record.StudentId);

            if (totalSessions > 0)
            {
                decimal percentage = (decimal)presentSessions / totalSessions * 100;

                // Cập nhật vào bảng Enrollment (tương đương StudentCourse của bạn)
                var enrollment = await _enrollmentRepository.GetByStudentAndClassAsync(record.StudentId, request.ClassId);
                if (enrollment != null)
                {
                    enrollment.AttendancePercentage = Math.Round(percentage, 2);
                    _enrollmentRepository.Update(enrollment);
                }
            }
        }

        // Lưu lại tỷ lệ chuyên cần
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<bool>.Success(true, "Điểm danh và cập nhật phần trăm thành công.");
    }
}

