using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs.Attendances;
using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Services;

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

    public async Task<ServiceResult<bool>> CreateAttendanceAsync(CreateAttendanceRequest request)
    {
        var attendance = new Attendance
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            AttendanceDate = request.AttendanceDate,
            Status = request.Status,
            Note = request.Note
        };

        await _attendanceRepository.AddAsync(attendance);
        await _attendanceRepository.SaveChangesAsync();

        await RecalculateAttendancePercentageAsync(request.ClassId, request.StudentId);

        return ServiceResult<bool>.Success(true, "Thêm điểm danh thành công.");
    }

    public async Task<ServiceResult<bool>> UpdateAttendanceAsync(int id, UpdateAttendanceRequest request)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);
        if (attendance == null) return ServiceResult<bool>.NotFound("Không tìm thấy dữ liệu điểm danh.");

        attendance.Status = request.Status;
        attendance.Note = request.Note;
        attendance.UpdatedAt = DateTime.UtcNow;

        _attendanceRepository.Update(attendance);
        await _attendanceRepository.SaveChangesAsync();

        await RecalculateAttendancePercentageAsync(attendance.ClassId, attendance.StudentId);

        return ServiceResult<bool>.Success(true, "Cập nhật điểm danh thành công.");
    }

    public async Task<ServiceResult<List<StudentAttendanceResponse>>> GetAttendancesByClassAsync(int classId)
    {
        var attendances = await _attendanceRepository.GetByClassAsync(classId);
        var result = new List<StudentAttendanceResponse>();

        foreach (var a in attendances)
        {
            var student = await _enrollmentRepository.GetStudentByIdAsync(a.StudentId);
            result.Add(new StudentAttendanceResponse
            {
                Id = a.Id,
                StudentId = a.StudentId,
                ClassId = a.ClassId,
                StudentName = student?.FullName ?? "Unknown",
                AttendanceDate = a.AttendanceDate,
                Status = a.Status,
                Note = a.Note
            });
        }

        return ServiceResult<List<StudentAttendanceResponse>>.Success(result, "Lấy danh sách thành công.");
    }

    public async Task<ServiceResult<List<StudentAttendanceResponse>>> GetAttendancesByStudentAndClassAsync(int studentId, int classId)
    {
        var attendances = await _attendanceRepository.GetByStudentAndClassAsync(studentId, classId);
        var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
        var result = attendances.Select(a => new StudentAttendanceResponse
        {
            Id = a.Id,
            StudentId = a.StudentId,
            ClassId = a.ClassId,
            StudentName = student?.FullName ?? "Unknown",
            AttendanceDate = a.AttendanceDate,
            Status = a.Status,
            Note = a.Note
        }).ToList();

        return ServiceResult<List<StudentAttendanceResponse>>.Success(result, "Lấy danh sách điểm danh thành công.");
    }

    public async Task<ServiceResult<AttendanceSummaryResponse>> GetAttendanceSummaryAsync(int studentId, int classId)
    {
        var student = await _enrollmentRepository.GetStudentByIdAsync(studentId);
        var total = await _attendanceRepository.GetTotalSessionsAsync(classId, studentId);
        var present = await _attendanceRepository.GetPresentSessionsAsync(classId, studentId);
        var percentage = total > 0 ? Math.Round((decimal)present / total * 100, 2) : 0;

        var response = new AttendanceSummaryResponse
        {
            StudentId = studentId,
            ClassId = classId,
            StudentName = student?.FullName ?? "Unknown",
            TotalSessions = total,
            PresentSessions = present,
            AttendancePercentage = percentage
        };

        return ServiceResult<AttendanceSummaryResponse>.Success(response, "Lấy tổng kết thành công.");
    }

    private async Task RecalculateAttendancePercentageAsync(int classId, int studentId)
    {
        var totalSessions = await _attendanceRepository.GetTotalSessionsAsync(classId, studentId);
        var presentSessions = await _attendanceRepository.GetPresentSessionsAsync(classId, studentId);

        if (totalSessions > 0)
        {
            decimal percentage = (decimal)presentSessions / totalSessions * 100;

            var enrollment = await _enrollmentRepository.GetByStudentAndClassAsync(studentId, classId);
            if (enrollment != null)
            {
                enrollment.AttendancePercentage = Math.Round(percentage, 2);
                _enrollmentRepository.Update(enrollment);
                await _enrollmentRepository.SaveChangesAsync();
            }
        }
    }
}

