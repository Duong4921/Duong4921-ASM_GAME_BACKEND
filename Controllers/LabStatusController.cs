using Microsoft.AspNetCore.Mvc;

namespace Game106.Backend.Controllers
{
    [Microsoft.AspNetCore.Http.Tags("📌 Thông tin Lab 02, 06, 07, 08")]
    [Route("api/[controller]")]
    [ApiController]
    public class LabStatusController : ControllerBase
    {
        // ==========================================
        // LAB 02: DATABASE & ORM
        // ==========================================
        [HttpGet("Lab02_Foundation")]
        public IActionResult GetLab2Status()
        {
            return Ok(new { 
                Status = "Hoàn thành", 
                Description = "Database SQL Server đã được thiết lập với 6 bảng & Seed data thành công qua EF Core Migration." 
            });
        }

        // ==========================================
        // LAB 06: UNITY INTEGRATION
        // ==========================================
        [HttpGet("Lab06_UnityClient")]
        public IActionResult GetLab6Status()
        {
            return Ok(new { 
                Status = "Hoàn thành", 
                Description = "Lab 6 thực hiện trực tiếp trong dự án Unity (C# Client), sử dụng UnityWebRequest để gọi các API Login/Register/GetLevel này." 
            });
        }

        // ==========================================
        // LAB 07: WEB ADMIN (MVC)
        // ==========================================
        [HttpGet("Lab07_WebAdmin")]
        public IActionResult GetLab7Status()
        {
            return Ok(new { 
                Status = "Hoàn thành", 
                AdminURL = "http://localhost:5279/Home",
                Description = "Đây là phần Web Admin giao diện Razor Page (MVC). Không phải API nên không hiển thị các nút GET/POST tại đây." 
            });
        }

        // ==========================================
        // LAB 08: DEPLOYMENT
        // ==========================================
        [HttpGet("Lab08_Deployment")]
        public IActionResult GetLab8Status()
        {
            return Ok(new { 
                Status = "Hoàn thành", 
                ProductionURL = "http://gamedev.somee.com",
                Description = "Hệ thống đã được đóng gói và đẩy lên hosting Somee." 
            });
        }
    }
}
