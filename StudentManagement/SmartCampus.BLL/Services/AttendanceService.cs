using SmartCampus.BLL.Constants;
using SmartCampus.BLL.DTOs.Attendances;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;

namespace SmartCampus.BLL.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<ServiceResult<AttendanceResponse>> CreateAsync(CreateAttendanceRequest request)
    {
        var validationMessage = ValidateStatus(request.Status);
        if (validationMessage is not null)
        {
            return ServiceResult<AttendanceResponse>.BadRequest(validationMessage);
        }

        var student = await _attendanceRepository.GetStudentByIdAsync(request.StudentId);
        if (student is null)
        {
            return ServiceResult<AttendanceResponse>.NotFound("Khong tim thay hoc vien.");
        }

        var hasEnrollment = await _attendanceRepository.HasActiveEnrollmentAsync(request.StudentId, request.ClassId);
        if (!hasEnrollment)
        {
            return ServiceResult<AttendanceResponse>.Conflict("Hoc vien chua dang ky lop nay.");
        }

        var existingAttendance = await _attendanceRepository.GetByStudentClassAndDateAsync(
            request.StudentId,
            request.ClassId,
            request.AttendanceDate);

        if (existingAttendance is not null)
        {
            return ServiceResult<AttendanceResponse>.Conflict("Hoc vien da duoc diem danh trong ngay nay.");
        }

        var attendance = new Attendance
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            AttendanceDate = request.AttendanceDate,
            Status = request.Status.Trim(),
            Note = request.Note
        };

        await _attendanceRepository.AddAsync(attendance);
        await _attendanceRepository.SaveChangesAsync();

        return ServiceResult<AttendanceResponse>.Success(
            MapAttendance(attendance),
            "Diem danh thanh cong.");
    }

    public async Task<ServiceResult<BulkAttendanceResponse>> CreateBulkAsync(CreateBulkAttendanceRequest request)
    {
        if (request.Students.Count == 0)
        {
            return ServiceResult<BulkAttendanceResponse>.BadRequest("Danh sach hoc vien diem danh khong duoc de trong.");
        }

        var duplicatedStudentIds = request.Students
            .GroupBy(student => student.StudentId)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        if (duplicatedStudentIds.Count > 0)
        {
            return ServiceResult<BulkAttendanceResponse>.Conflict("Danh sach diem danh co hoc vien bi trung.");
        }

        var attendances = new List<Attendance>();
        foreach (var item in request.Students)
        {
            var validationMessage = ValidateStatus(item.Status);
            if (validationMessage is not null)
            {
                return ServiceResult<BulkAttendanceResponse>.BadRequest(
                    $"Hoc vien {item.StudentId}: {validationMessage}");
            }

            var student = await _attendanceRepository.GetStudentByIdAsync(item.StudentId);
            if (student is null)
            {
                return ServiceResult<BulkAttendanceResponse>.NotFound(
                    $"Khong tim thay hoc vien {item.StudentId}.");
            }

            var hasEnrollment = await _attendanceRepository.HasActiveEnrollmentAsync(item.StudentId, request.ClassId);
            if (!hasEnrollment)
            {
                return ServiceResult<BulkAttendanceResponse>.Conflict(
                    $"Hoc vien {item.StudentId} chua dang ky lop nay.");
            }

            var existingAttendance = await _attendanceRepository.GetByStudentClassAndDateAsync(
                item.StudentId,
                request.ClassId,
                request.AttendanceDate);

            if (existingAttendance is not null)
            {
                return ServiceResult<BulkAttendanceResponse>.Conflict(
                    $"Hoc vien {item.StudentId} da duoc diem danh trong ngay nay.");
            }

            attendances.Add(new Attendance
            {
                StudentId = item.StudentId,
                ClassId = request.ClassId,
                AttendanceDate = request.AttendanceDate,
                Status = item.Status.Trim(),
                Note = item.Note
            });
        }

        await _attendanceRepository.AddRangeAsync(attendances);
        await _attendanceRepository.SaveChangesAsync();

        var response = new BulkAttendanceResponse
        {
            ClassId = request.ClassId,
            AttendanceDate = request.AttendanceDate,
            CreatedCount = attendances.Count,
            CreatedAttendances = attendances.Select(MapAttendance).ToList()
        };

        return ServiceResult<BulkAttendanceResponse>.Success(
            response,
            "Diem danh hang loat thanh cong.");
    }

    public async Task<ServiceResult<AttendanceResponse>> UpdateAsync(
        int attendanceId,
        UpdateAttendanceRequest request)
    {
        var validationMessage = ValidateStatus(request.Status);
        if (validationMessage is not null)
        {
            return ServiceResult<AttendanceResponse>.BadRequest(validationMessage);
        }

        var attendance = await _attendanceRepository.GetByIdAsync(attendanceId);
        if (attendance is null)
        {
            return ServiceResult<AttendanceResponse>.NotFound("Khong tim thay ban ghi diem danh.");
        }

        attendance.Status = request.Status.Trim();
        attendance.Note = request.Note;
        await _attendanceRepository.SaveChangesAsync();

        return ServiceResult<AttendanceResponse>.Success(
            MapAttendance(attendance),
            "Cap nhat diem danh thanh cong.");
    }

    public async Task<IReadOnlyList<AttendanceResponse>> GetByClassIdAsync(int classId)
    {
        var attendances = await _attendanceRepository.GetByClassIdAsync(classId);

        return attendances.Select(MapAttendance).ToList();
    }

    public async Task<IReadOnlyList<AttendanceResponse>> GetByStudentAndClassAsync(int studentId, int classId)
    {
        var attendances = await _attendanceRepository.GetByStudentAndClassAsync(studentId, classId);

        return attendances.Select(MapAttendance).ToList();
    }

    public async Task<ServiceResult<AttendanceSummaryResponse>> GetSummaryAsync(int studentId, int classId)
    {
        var student = await _attendanceRepository.GetStudentByIdAsync(studentId);
        if (student is null)
        {
            return ServiceResult<AttendanceSummaryResponse>.NotFound("Khong tim thay hoc vien.");
        }

        var hasEnrollment = await _attendanceRepository.HasActiveEnrollmentAsync(studentId, classId);
        if (!hasEnrollment)
        {
            return ServiceResult<AttendanceSummaryResponse>.Conflict("Hoc vien chua dang ky lop nay.");
        }

        var attendances = await _attendanceRepository.GetByStudentAndClassAsync(studentId, classId);
        var summary = CalculateSummary(studentId, classId, attendances);

        return ServiceResult<AttendanceSummaryResponse>.Success(
            summary,
            "Tinh ty le chuyen can thanh cong.");
    }

    private static string? ValidateStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return "Trang thai diem danh khong duoc de trong.";
        }

        return AttendanceStatuses.Allowed.Contains(status.Trim())
            ? null
            : "Trang thai diem danh khong hop le.";
    }

    public static AttendanceSummaryResponse CalculateSummary(
        int studentId,
        int classId,
        IReadOnlyCollection<Attendance> attendances)
    {
        var totalSessions = attendances.Count;
        var attendedSessions = attendances.Count(attendance => AttendanceStatuses.IsAttended(attendance.Status));
        var attendancePercentage = totalSessions == 0
            ? 0
            : Math.Round(attendedSessions * 100m / totalSessions, 2);

        return new AttendanceSummaryResponse
        {
            StudentId = studentId,
            ClassId = classId,
            TotalSessions = totalSessions,
            AttendedSessions = attendedSessions,
            AttendancePercentage = attendancePercentage
        };
    }

    private static AttendanceResponse MapAttendance(Attendance attendance)
    {
        return new AttendanceResponse
        {
            Id = attendance.Id,
            StudentId = attendance.StudentId,
            ClassId = attendance.ClassId,
            AttendanceDate = attendance.AttendanceDate,
            Status = attendance.Status,
            Note = attendance.Note
        };
    }
}
