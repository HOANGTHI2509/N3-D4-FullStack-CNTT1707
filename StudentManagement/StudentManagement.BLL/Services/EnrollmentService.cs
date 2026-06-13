using StudentManagement.BLL.DTOs.Enrollments;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories;

namespace StudentManagement.BLL.Services;

public class EnrollmentService : IEnrollmentService
{
    private const string ActiveStatus = "Đang học";
    private const string CancelledStatus = "Đã hủy";

    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<ServiceResult<EnrollmentResponse>> CreateAsync(CreateEnrollmentRequest request)
    {
        var student = await _enrollmentRepository.GetStudentByIdAsync(request.StudentId);
        if (student is null)
        {
            return ServiceResult<EnrollmentResponse>.NotFound("Không tìm thấy học viên.");
        }

        var existingEnrollment = await _enrollmentRepository.GetByStudentAndClassAsync(
            request.StudentId,
            request.ClassId);

        if (existingEnrollment is not null)
        {
            if (!string.Equals(existingEnrollment.Status, CancelledStatus, StringComparison.OrdinalIgnoreCase))
            {
                return ServiceResult<EnrollmentResponse>.Conflict("Học viên đã đăng ký lớp này.");
            }

            existingEnrollment.Status = ActiveStatus;
            existingEnrollment.EnrollmentDate = DateTime.UtcNow;
            await _enrollmentRepository.SaveChangesAsync();

            return ServiceResult<EnrollmentResponse>.Success(
                MapEnrollment(existingEnrollment),
                "Đăng ký lại lớp học thành công.");
        }

        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            EnrollmentDate = DateTime.UtcNow,
            Status = ActiveStatus
        };

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<EnrollmentResponse>.Success(
            MapEnrollment(enrollment),
            "Đăng ký lớp học thành công.");
    }

    public async Task<ServiceResult<EnrollmentResponse>> CancelAsync(int enrollmentId)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
        if (enrollment is null)
        {
            return ServiceResult<EnrollmentResponse>.NotFound("Không tìm thấy đăng ký lớp học.");
        }

        if (string.Equals(enrollment.Status, CancelledStatus, StringComparison.OrdinalIgnoreCase))
        {
            return ServiceResult<EnrollmentResponse>.Conflict("Đăng ký lớp học đã được hủy trước đó.");
        }

        enrollment.Status = CancelledStatus;
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<EnrollmentResponse>.Success(
            MapEnrollment(enrollment),
            "Hủy đăng ký lớp học thành công.");
    }

    public async Task<IReadOnlyList<ClassStudentResponse>> GetStudentsByClassIdAsync(int classId)
    {
        var enrollments = await _enrollmentRepository.GetByClassIdAsync(classId);

        return enrollments
            .Select(enrollment => new ClassStudentResponse
            {
                EnrollmentId = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentCode = enrollment.Student.StudentCode,
                FullName = enrollment.Student.FullName,
                Email = enrollment.Student.Email,
                Status = enrollment.Status ?? string.Empty
            })
            .ToList();
    }

    private static EnrollmentResponse MapEnrollment(Enrollment enrollment)
    {
        return new EnrollmentResponse
        {
            Id = enrollment.Id,
            StudentId = enrollment.StudentId,
            ClassId = enrollment.ClassId,
            EnrollmentDate = enrollment.EnrollmentDate,
            Status = enrollment.Status ?? string.Empty
        };
    }
}

