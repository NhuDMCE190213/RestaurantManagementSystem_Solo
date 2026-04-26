namespace RestaurantManagement.Application.Interfaces
{
    /// <summary>
    /// Cung cấp các phương thức để tạo JWT token và refresh token
    /// </summary>
    public interface IJwtTokenProvider
    {
        string GenerateToken(Guid userId, string email, string userName, string role);
        string GenerateRefreshToken();
    }
}
