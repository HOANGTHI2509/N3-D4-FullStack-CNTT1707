using SmartCampus.BLL.DTOs.Enrollments;

namespace SmartCampus.BLL.Services;

public interface IEnrollmentService
{
    Task<ServiceResult<EnrollmentResponse>> CreateAsync(CreateEnrollmentRequest request);

    Task<ServiceResult<EnrollmentResponse>> CancelAsync(int enrollmentId);

    Task<IReadOnlyList<ClassStudentResponse>> GetStudentsByClassIdAsync(int classId);
}
