using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs.Enrollments;

namespace StudentManagement.Application.Services;

public interface IEnrollmentService
{
    Task<ServiceResult<EnrollmentResponse>> CreateAsync(CreateEnrollmentRequest request);

    Task<ServiceResult<EnrollmentResponse>> CancelAsync(int enrollmentId);

    Task<IReadOnlyList<ClassStudentResponse>> GetStudentsByClassIdAsync(int classId);
}

