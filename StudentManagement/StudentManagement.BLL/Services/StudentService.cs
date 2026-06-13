using StudentManagement.BLL.DTOs;
using StudentManagement.BLL.DTOs.Students;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories;

namespace StudentManagement.BLL.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ServiceResult<PagedResponse<StudentResponse>>> GetPagedAsync(int page, int pageSize, string? searchTerm)
    {
        var totalCount = await _studentRepository.GetTotalCountAsync(searchTerm);
        var students = await _studentRepository.GetPagedAsync(page, pageSize, searchTerm);
        
        var responses = students.Select(s => new StudentResponse
        {
            Id = s.Id,
            StudentCode = s.StudentCode,
            FullName = s.FullName,
            Email = s.Email,
            Status = s.Status,
            Major = s.Major
        }).ToList();

        var pagedResponse = new PagedResponse<StudentResponse>
        {
            Items = responses,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return ServiceResult<PagedResponse<StudentResponse>>.Success(pagedResponse, "Lấy danh sách thành công.");
    }

    public async Task<ServiceResult<StudentResponse>> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null)
        {
            return ServiceResult<StudentResponse>.NotFound($"Không tìm thấy học viên với ID {id}");
        }

        var response = new StudentResponse
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            FullName = student.FullName,
            Email = student.Email,
            Status = student.Status,
            Major = student.Major
            // Ánh xạ thêm các trường khác
        };

        return ServiceResult<StudentResponse>.Success(response, "Thành công");
    }

    public async Task<ServiceResult<StudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        // 1. Validate trùng Mã Sinh Viên
        var existingCode = await _studentRepository.GetByStudentCodeAsync(request.StudentCode);
        if (existingCode != null)
        {
            return ServiceResult<StudentResponse>.Conflict($"Mã sinh viên '{request.StudentCode}' đã tồn tại trong hệ thống.");
        }

        // 2. Validate trùng CCCD (nếu có nhập)
        if (!string.IsNullOrEmpty(request.IdentityCardNumber))
        {
            var existingIdCard = await _studentRepository.GetByIdentityCardAsync(request.IdentityCardNumber);
            if (existingIdCard != null)
            {
                return ServiceResult<StudentResponse>.Conflict($"Số CCCD '{request.IdentityCardNumber}' đã được sử dụng.");
            }
        }

        // 3. Khởi tạo Entity
        var newStudent = new Student
        {
            StudentCode = request.StudentCode,
            IdentityCardNumber = request.IdentityCardNumber,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            AvatarUrl = request.AvatarUrl,
            Major = request.Major,
            Status = "Active", // Mặc định khi tạo mới
            EnrollmentDate = DateOnly.FromDateTime(DateTime.UtcNow),
            CreatedAt = DateTime.UtcNow
        };

        // 4. Lưu vào DB
        await _studentRepository.AddAsync(newStudent);
        await _studentRepository.SaveChangesAsync();

        // 5. Trả về kết quả
        var response = new StudentResponse
        {
            Id = newStudent.Id,
            StudentCode = newStudent.StudentCode,
            FullName = newStudent.FullName,
            Email = newStudent.Email,
            Status = newStudent.Status,
            Major = newStudent.Major
        };

        return ServiceResult<StudentResponse>.Success(response, "Thêm học viên thành công.");
    }

    public async Task<ServiceResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null || student.IsDeleted)
        {
            return ServiceResult<StudentResponse>.NotFound($"Không tìm thấy học viên với ID {id}");
        }

        // Cập nhật thông tin
        if (request.PhoneNumber != null) student.PhoneNumber = request.PhoneNumber;
        if (request.Address != null) student.Address = request.Address;
        if (request.Email != null) student.Email = request.Email;
        
        student.UpdatedAt = DateTime.UtcNow;

        _studentRepository.Update(student);
        await _studentRepository.SaveChangesAsync();

        var response = new StudentResponse
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            FullName = student.FullName,
            Email = student.Email,
            Status = student.Status,
            Major = student.Major
        };

        return ServiceResult<StudentResponse>.Success(response, "Cập nhật thành công.");
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null || student.IsDeleted)
        {
            return ServiceResult<bool>.NotFound($"Không tìm thấy học viên với ID {id}");
        }

        // Xóa mềm
        student.IsDeleted = true;
        student.Status = "Inactive";
        student.UpdatedAt = DateTime.UtcNow;

        _studentRepository.Update(student);
        await _studentRepository.SaveChangesAsync();

        return ServiceResult<bool>.Success(true, "Xóa mềm học viên thành công.");
    }
}

