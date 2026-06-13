using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs.Attendances;

namespace StudentManagement.Application.Services;

public interface IAttendanceService
{
    Task<ServiceResult<bool>> SubmitAttendanceAsync(SubmitAttendanceRequest request);
    Task<ServiceResult<bool>> CreateAttendanceAsync(CreateAttendanceRequest request);
    Task<ServiceResult<bool>> UpdateAttendanceAsync(int id, UpdateAttendanceRequest request);
    Task<ServiceResult<List<StudentAttendanceResponse>>> GetAttendancesByClassAsync(int classId);
    Task<ServiceResult<List<StudentAttendanceResponse>>> GetAttendancesByStudentAndClassAsync(int studentId, int classId);
    Task<ServiceResult<AttendanceSummaryResponse>> GetAttendanceSummaryAsync(int studentId, int classId);
}

