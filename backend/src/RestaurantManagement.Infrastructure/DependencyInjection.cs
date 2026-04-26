using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }

            services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(connectionString); // Sử dụng PostgreSQL làm database provider
                options.UseSnakeCaseNamingConvention(); // Sử dụng quy tắc đặt tên snake_case cho các bảng và cột trong database
            });

            return services;
        }
    }
}
