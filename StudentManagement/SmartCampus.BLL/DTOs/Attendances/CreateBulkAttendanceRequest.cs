using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Attendances;

public class CreateBulkAttendanceRequest
{
    [Range(1, int.MaxValue)]
    public int ClassId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    [MinLength(1)]
    public List<BulkAttendanceItemRequest> Students { get; set; } = [];
}
