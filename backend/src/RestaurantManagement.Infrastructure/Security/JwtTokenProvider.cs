using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantManagement.Infrastructure.Security
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationInMinutes;

        /// <summary>
        /// Khởi tạo JwtTokenProvider với cấu hình từ .env
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public JwtTokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey is not configured");
            _issuer = _configuration["JwtSettings:Issuer"]
                ?? throw new InvalidOperationException("JwtSettings:Issuer is not configured");
            _audience = _configuration["JwtSettings:Audience"]
                ?? throw new InvalidOperationException("JwtSettings:Audience is not configured");

            if (!int.TryParse(_configuration["JwtSettings:ExpirationMinutes"], out _expirationInMinutes))
            {
                // Nếu không thể parse được giá trị expiration, đặt mặc định là 60 phút
                _expirationInMinutes = 60;
            }
        }

        /// <summary>
        /// Tạo một refresh token ngẫu nhiên có độ dài 64 byte và được mã hóa bằng Base64
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            // Sử dụng RandomNumberGenerator để tạo một chuỗi ngẫu nhiên an toàn
            // Sử dụng System.Security.Cryptography.RandomNumberGenerator để tạo một chuỗi ngẫu nhiên an toàn
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }   
        }

        /// <summary>
        /// Tạo một JWT token mới với thông tin người dùng và vai trò được truyền vào
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GenerateToken(Guid userId, string email, string userName, string role)
        {
            // Tạo một khóa bảo mật từ secret key và sử dụng HMAC SHA256 để ký token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            // Tạo một đối tượng SigningCredentials để xác định thuật toán ký và khóa bảo mật
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo một danh sách các claim để lưu trữ thông tin người dùng và vai trò
            // Dùng Claim bởi vì nó là một cách chuẩn để lưu trữ thông tin trong JWT token và có thể dễ dàng truy cập sau này
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
