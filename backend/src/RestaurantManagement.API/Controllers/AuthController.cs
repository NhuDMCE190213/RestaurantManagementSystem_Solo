using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public AuthController(IJwtTokenProvider jwtTokenProvider)
        {
            _jwtTokenProvider = jwtTokenProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var userId = Guid.NewGuid(); // Giả sử lấy userId từ database
            var userName = "John Doe"; // Giả sử lấy userName từ database
            var email = request.Email; // Giả sử lấy email từ database
            var role = "Admin"; // Giả sử lấy role từ database

            // Tạo JWT token và refresh token
            var accessToken = _jwtTokenProvider.GenerateToken(userId, email, userName, role);
            var refreshToken = _jwtTokenProvider.GenerateRefreshToken();


            // Trả về token và refresh token cho client
            return Ok(new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600, // Thời gian hết hạn của access token (ví dụ: 1 giờ)
                TokenType = "Bearer"
            });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Refresh token is required.");
            }
            // Kiểm tra tính hợp lệ của refresh token (ví dụ: so sánh với giá trị lưu trong database)
            // Nếu refresh token hợp lệ, tạo một access token mới
            var userId = Guid.NewGuid(); // Giả sử lấy userId từ database
            var email = "user@example.com"; // Giả sử lấy email từ database
            var userName = "John Doe"; // Giả sử lấy userName từ database
            var role = "Admin"; // Giả sử lấy role từ database

            var accessToken = _jwtTokenProvider.GenerateToken(userId, email, userName, role);

            return Ok(new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = request.RefreshToken, // Trả lại refresh token cũ hoặc tạo mới nếu cần
                ExpiresIn = 3600, // Thời gian hết hạn của access token (ví dụ: 1 giờ)
                TokenType = "Bearer"
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
