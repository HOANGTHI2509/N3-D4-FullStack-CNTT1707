using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Services;

public interface IStudentService
{
    Task<ServiceResult<PagedResponse<StudentResponse>>> GetPagedAsync(int page, int pageSize, string? searchTerm);
    Task<ServiceResult<StudentResponse>> GetByIdAsync(int id);
    Task<ServiceResult<StudentResponse>> CreateAsync(CreateStudentRequest request);
    Task<ServiceResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}

