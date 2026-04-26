using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.API.Utilities;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure;
using RestaurantManagement.Infrastructure.Seed;
using System.Text;

// Tải các biến môi trường từ file .env vào ứng dụng
DotNetEnv.Env.Load(".env");

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container.
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration); // Đăng ký các dịch vụ liên quan đến cơ sở dữ liệu và repository từ lớp Infrastructure

// Thêm các dịch vụ cho Swagger/OpenAPI để tự động tạo tài liệu API và giao diện người dùng để thử nghiệm API
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Cấu hình Jwt Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");  // Lấy các cài đặt liên quan đến JWT từ cấu hình
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] 
    ?? throw new InvalidOperationException("JwtSettings:SecretKey is not configured"));

builder.Services
    .AddAuthentication("Bearer") // Sử dụng scheme "Bearer" cho JWT
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters // Cấu hình các tham số để xác thực token
        {
            ValidateIssuerSigningKey = true, // Bật xác thực khóa ký của token
            IssuerSigningKey = new SymmetricSecurityKey(secretKey), // Sử dụng khóa bí mật để xác thực token
            ValidateIssuer = true, // Bật xác thực issuer của token
            ValidIssuer = jwtSettings["Issuer"], // Kiểm tra issuer của token
            ValidateAudience = true, // Bật xác thực audience của token
            ValidAudience = jwtSettings["Audience"], // Kiểm tra audience của token
            ValidateLifetime = true,    // Kiểm tra thời gian hết hạn của token
            ClockSkew = TimeSpan.Zero // Loại bỏ thời gian chênh lệch mặc định ví dụ như 5 phút để token vẫn được chấp nhận sau khi hết hạn
        };
    });

builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>(); // Đăng ký dịch vụ JwtTokenProvider để có thể sử dụng trong các controller

// Cấu hình CORS để cho phép các yêu cầu từ frontend (ví dụ: http://localhost:3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // Lấy danh sách các origin được phép từ cấu hình, nếu không có thì sử dụng mặc định
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
        ?? new[] { "http://localhost:5173", "http://localhost:3000" };

        // Cấu hình chính sách CORS để cho phép các yêu cầu từ các origin được phép, cho phép tất cả các header và phương thức, và cho phép gửi cookie
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader() // Cho phép tất cả các header trong các yêu cầu CORS
              .AllowAnyMethod() // Cho phép tất cả các phương thức HTTP (GET, POST, PUT, DELETE, v.v.) trong các yêu cầu CORS
              .AllowCredentials(); // Cho phép gửi cookie và thông tin xác thực trong các yêu cầu CORS
    });
});

var app = builder.Build();


// Seed dữ liệu mẫu vào cơ sở dữ liệu khi ứng dụng khởi động. Điều này sẽ giúp có dữ liệu để kiểm thử API ngay sau khi chạy ứng dụng.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(context);
}

// Cấu hình pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Thêm middleware CORS để áp dụng chính sách CORS đã cấu hình
app.UseAuthentication(); // Thêm middleware xác thực để kiểm tra JWT token trong các yêu cầu đến
app.UseAuthorization();
app.MapControllers();

app.Run();
