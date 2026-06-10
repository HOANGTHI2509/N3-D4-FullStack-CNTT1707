using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartCampus.Controllers
{
    [ApiController]
    [Route("api/v1/attendances")] // Đã sửa lại theo chuẩn Swagger
    public class AttendanceController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult MarkAttendance()
        {
            return Ok(new
            {
                success = true,
                message = "Điểm danh thành công. Xin chào Admin/Teacher!",
                data = default(object),
                errors = (object)null // Bổ sung cho đúng chuẩn Convention
            });
        }
    }
}