using Microsoft.EntityFrameworkCore;
using Game106.Backend.Context;
using Game106.Backend.Services;
using Game106.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Game106 API", Version = "v1" });

    // Hướng dẫn Swagger sử dụng Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập Token vào ô dưới đây (ví dụ: Bearer [yourToken])",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ----- THÊM ĐOẠN NÀY LÀ XONG PHẦN CẤU HÌNH -----
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Identity - hệ thống quản lý đăng ký/đăng nhập người dùng
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<GameDbContext>()
.AddDefaultTokenProviders();

// Enable Cookie authentication for MVC (Lab 7)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

// Disable JWT temporarily or keep it alongside, but MVC controllers will use Cookie
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)... (Đã cấu hình JWT trước đó, có thể tắt để dùng nguyên Cookie cho dễ)

builder.Services.AddControllersWithViews(); // Phục vụ MVC Web Admin
// -----------------------------------------------
// Thêm dịch vụ Razor Render ở dạng Singleton (vì engine nên được tái sinh/cache lại 1 lần để tăng hiệu suất)
builder.Services.AddSingleton<IRazorRenderService, RazorRenderService>();

// Cấu hình Email
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

// Cấu hình JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    // Tạm thời tắt Default JWT scheme theo yêu cầu của Lab 7 để ưu tiên sử dụng MVC Cookie
    // options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();


// Bật Swagger UI ở cả Local và Production (Somee) để Giảng viên có thể Test API trực tiếp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game106 API v1");
    c.RoutePrefix = "swagger"; // Truy cập qua: http://domain.com/swagger
});

if (app.Environment.IsDevelopment())
{
    // Các cấu hình chỉ dành cho môi trường Dev (nếu có)
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép truy cập ảnh trong wwwroot (như avatar)

app.UseAuthentication(); // Bật xác thực trước Authorization
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Tự động tạo Role "Admin" vào Db nếu chưa có (Yêu cầu của Lab 7)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
}

app.Run();
