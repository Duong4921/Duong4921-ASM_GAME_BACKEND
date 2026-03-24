using Microsoft.EntityFrameworkCore;
using Game106.Backend.Context; // <--- Thêm using này lên cùng file
using Game106.Backend.Services; // Nhớ using namespace
using Game106.Backend.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----- THÊM ĐOẠN NÀY LÀ XONG PHẦN CẤU HÌNH -----
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// -----------------------------------------------
// Thêm dịch vụ Razor Render ở dạng Singleton (vì engine nên được tái sinh/cache lại 1 lần để tăng hiệu suất)
builder.Services.AddSingleton<IRazorRenderService, RazorRenderService>();

// Cấu hình Email
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();


// Khởi chạy Swagger UI khi ấn "Play"
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Tự động chuyển hướng từ trang chủ sang Swagger cho tiện
app.MapGet("/", context => {
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
