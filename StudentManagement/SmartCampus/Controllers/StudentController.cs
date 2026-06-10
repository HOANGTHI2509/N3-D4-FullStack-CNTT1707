using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartCampus.Controllers
{
    [ApiController]
    [Route("api/v1/students")] // Đã sửa lại theo chuẩn Swagger
    public class StudentController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public IActionResult GetMyProfile()
        {
            return Ok(new
            {
                success = true,
                message = "Lấy thông tin cá nhân thành công. Xin chào Student!",
                data = default(object),
                errors = (object)null // Bổ sung cho đúng chuẩn Convention
            });
        }
    }
}