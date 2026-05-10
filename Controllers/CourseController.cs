using Microsoft.AspNetCore.Mvc;
using Game106.Backend.Models;

namespace Game106.Backend.Controllers
{
    [Microsoft.AspNetCore.Http.Tags("Lab 01 - Thiết lập & Nền tảng")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // ==========================================
        // LAB 01 - BÀI 2: Viết function Get trả về chuỗi văn bản đơn giản
        // ==========================================
        [HttpGet("GetText")]
        public IActionResult GetText()
        {
            return Ok("Hello, this is the first API running on ASP.NET Core MVC!");
        }

        // ==========================================
        // LAB 01 - BÀI 3: Tạo Model dữ liệu cơ bản và trả về JSON
        // ==========================================
        [HttpGet("GetCourseInfo")]
        public IActionResult GetCourseInfo()
        {
            var course = new CourseModel
            {
                CourseName = "Lập trình Game Back-End",
                CourseCode = "GAM106",
                Name = "Phạm Đức Dương",
                StudentCode = "PH63177",
                Class = "GA21101"
            };

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Get data success",
                Data = course
            });
        }
    }
}
