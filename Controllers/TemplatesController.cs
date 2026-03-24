using Microsoft.AspNetCore.Mvc;
using Game106.Backend.Services;
using Game106.Backend.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly IRazorRenderService _razorRenderService;
    private readonly IEmailService _emailService;

    // Nạp dịch vụ thông qua khởi tạo (Dependency Injection)
    public TemplatesController(IRazorRenderService razorRenderService, IEmailService emailService)
    {
        _razorRenderService = razorRenderService;
        _emailService = emailService;
    }

    [HttpGet("welcome")]
    public async Task<IActionResult> TestWelcomeTemplate()
    {
        // Tạo biến dữ liệu để đưa vào template
        var model = new WelcomeEmailModel 
        { 
            PlayerName = "Anh Hùng Lofi", 
            RegistrationScore = 2000 
        };

        // Kết xuất ra chuỗi HTML
        string htmlContent = await _razorRenderService.RenderTemplateAsync("WelcomeEmail.cshtml", model);
        
        // Trả ra dạng trang HTML trên API để xem tận mắt (hoặc bạn dùng htmlContent này để đưa vào thư viện gửi mail)
        return Content(htmlContent, "text/html", System.Text.Encoding.UTF8); 
    }

    [HttpGet("send-test")]
    public async Task<IActionResult> SendWelcomeEmailTest([FromQuery] string toEmail)
    {
        if (string.IsNullOrEmpty(toEmail))
        {
            return BadRequest("Vui lòng cung cấp email nhận bằng tham số ?toEmail=...");
        }

        var model = new WelcomeEmailModel 
        { 
            PlayerName = "Anh Hùng Lofi Test", 
            RegistrationScore = 5000 
        };

        // 1. Render HTML
        string htmlContent = await _razorRenderService.RenderTemplateAsync("WelcomeEmail.cshtml", model);
        
        // 2. Gửi thư đi
        await _emailService.SendEmailAsync(toEmail, "Chào mừng bạn gia nhập Game106!", htmlContent);
        
        return Ok($"Email đã được đưa vào luồng gửi thành công tới địa chỉ: {toEmail}. Hãy kiểm tra hòm thư của bạn!");
    }

    [HttpPost("send-teacher")]
    public async Task<IActionResult> SendToTeacher([FromBody] TeacherEmailRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.TeacherEmail))
        {
            return BadRequest("Thiếu thông tin Email của Giảng viên (TeacherEmail).");
        }

        // Tái sử dụng WelcomeEmailModel hoặc dùng dữ liệu chung
        var model = new WelcomeEmailModel 
        { 
            PlayerName = $"Thầy/Cô (Sinh viên: {request.StudentName})", 
            RegistrationScore = request.ProjectScore 
        };

        // Render HTML
        string htmlContent = await _razorRenderService.RenderTemplateAsync("WelcomeEmail.cshtml", model);
        
        // Thêm đoạn lời nhắn cá nhân vào dưới email nộp bài
        htmlContent += $"<br/><hr/><p><b>Lời nhắn từ sinh viên:</b> {request.Message}</p>";

        // Gửi thư đi
        await _emailService.SendEmailAsync(request.TeacherEmail, $"[Báo cáo đồ án Game106] Sinh viên {request.StudentName}", htmlContent);
        
        return Ok(new { message = "Đã gửi báo cáo thành công báo cáo bằng Postman!", to = request.TeacherEmail });
    }
}
