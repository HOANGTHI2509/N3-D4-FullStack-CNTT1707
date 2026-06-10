using SmartCampus.BLL.DTOs.Attendances;

namespace SmartCampus.BLL.Services;

public interface IAttendanceService
{
    Task<ServiceResult<AttendanceResponse>> CreateAsync(CreateAttendanceRequest request);

    Task<ServiceResult<BulkAttendanceResponse>> CreateBulkAsync(CreateBulkAttendanceRequest request);

    Task<ServiceResult<AttendanceResponse>> UpdateAsync(int attendanceId, UpdateAttendanceRequest request);

    Task<IReadOnlyList<AttendanceResponse>> GetByClassIdAsync(int classId);

    Task<IReadOnlyList<AttendanceResponse>> GetByStudentAndClassAsync(int studentId, int classId);

    Task<ServiceResult<AttendanceSummaryResponse>> GetSummaryAsync(int studentId, int classId);
}
