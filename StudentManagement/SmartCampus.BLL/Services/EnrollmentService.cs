using SmartCampus.BLL.DTOs.Enrollments;
using SmartCampus.DAL.Constants;
using SmartCampus.DAL.Models;
using SmartCampus.DAL.Repositories;

namespace SmartCampus.BLL.Services;

public class EnrollmentService : IEnrollmentService
{
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
            return ServiceResult<EnrollmentResponse>.NotFound("Khong tim thay hoc vien.");
        }

        var existingEnrollment = await _enrollmentRepository.GetByStudentAndClassAsync(
            request.StudentId,
            request.ClassId);

        if (existingEnrollment is not null)
        {
            if (!string.Equals(existingEnrollment.Status, EnrollmentStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
            {
                return ServiceResult<EnrollmentResponse>.Conflict("Hoc vien da dang ky lop nay.");
            }

            existingEnrollment.Status = EnrollmentStatuses.Active;
            existingEnrollment.EnrollmentDate = DateTime.UtcNow;
            await _enrollmentRepository.SaveChangesAsync();

            return ServiceResult<EnrollmentResponse>.Success(
                MapEnrollment(existingEnrollment),
                "Dang ky lai lop hoc thanh cong.");
        }

        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId,
            EnrollmentDate = DateTime.UtcNow,
            Status = EnrollmentStatuses.Active
        };

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<EnrollmentResponse>.Success(
            MapEnrollment(enrollment),
            "Dang ky lop hoc thanh cong.");
    }

    public async Task<ServiceResult<EnrollmentResponse>> CancelAsync(int enrollmentId)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
        if (enrollment is null)
        {
            return ServiceResult<EnrollmentResponse>.NotFound("Khong tim thay dang ky lop hoc.");
        }

        if (string.Equals(enrollment.Status, EnrollmentStatuses.Cancelled, StringComparison.OrdinalIgnoreCase))
        {
            return ServiceResult<EnrollmentResponse>.Conflict("Dang ky lop hoc da duoc huy truoc do.");
        }

        enrollment.Status = EnrollmentStatuses.Cancelled;
        await _enrollmentRepository.SaveChangesAsync();

        return ServiceResult<EnrollmentResponse>.Success(
            MapEnrollment(enrollment),
            "Huy dang ky lop hoc thanh cong.");
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
