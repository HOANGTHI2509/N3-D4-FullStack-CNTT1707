using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.DTOs.Grades;
using StudentManagement.Application.Services;

namespace StudentManagement.Application.Services;

public interface IGradeService
{
    Task<ServiceResult<bool>> SubmitBulkGradesAsync(SubmitGradeRequest request);
    Task<ServiceResult<bool>> CreateGradeAsync(CreateGradeRequest request);
    Task<ServiceResult<bool>> UpdateGradeAsync(int id, UpdateGradeRequest request);
    Task<ServiceResult<List<StudentGradeResponse>>> GetGradesByClassAsync(int classId);
    Task<ServiceResult<StudentGradeResponse>> GetStudentGradeAsync(int studentId, int classId);
}

