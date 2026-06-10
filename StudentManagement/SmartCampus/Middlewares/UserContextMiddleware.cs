using System.Security.Claims;

namespace SmartCampus.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra xem Request đã đi qua Authentication và hợp lệ chưa
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                // Ưu tiên tìm Claim chuẩn NameIdentifier, nếu không có thì tìm claim tên "id"
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier) 
                               ?? context.User.FindFirst("id");

                if (userIdClaim != null)
                {
                    // Lưu UserId vào HttpContext.Items để các tầng dưới (BLL) truy xuất dễ dàng
                    context.Items["UserId"] = userIdClaim.Value;
                }
            }

            // Luôn gọi delegate tiếp theo để pipeline không bị chặn
            await _next(context);
        }
    }
}
