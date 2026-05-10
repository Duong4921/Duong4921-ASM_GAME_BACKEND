using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Game106.Backend.Context;
using Game106.Backend.Models;
using Game106.Backend.DTO;
using Game106.Backend.ViewModel;
using Game106.Backend.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Game106.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIGameController : ControllerBase
    {
        private readonly GameDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        // Constructor - ASP.NET tự inject các dịch vụ vào đây
        public APIGameController(
            GameDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IWebHostEnvironment env,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _env = env;
            _config = config;
        }

        // =============================================
        // BÀI 2: APIs GET lấy dữ liệu
        // =============================================

        // API lấy toàn bộ cấp độ game
        // GET: api/APIGame/GetAllGameLevel
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("GetAllGameLevel")]
        public async Task<IActionResult> GetAllGameLevel()
        {
            var levels = await _context.GameLevels.ToListAsync();
            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lấy danh sách cấp độ thành công",
                Data = levels
            });
        }

        // API lấy toàn bộ câu hỏi trong database
        // GET: api/APIGame/GetAllQuestionGame
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("GetAllQuestionGame")]
        public async Task<IActionResult> GetAllQuestionGame()
        {
            var questions = await _context.Questions
                .Include(q => q.GameLevel) // JOIN bảng GameLevel để lấy tên cấp độ
                .ToListAsync();
            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lấy danh sách câu hỏi thành công",
                Data = questions
            });
        }

        // API lấy toàn bộ vùng
        // GET: api/APIGame/GetAllRegion
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("GetAllRegion")]
        public async Task<IActionResult> GetAllRegion()
        {
            var regions = await _context.Regions.ToListAsync();
            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lấy danh sách vùng thành công",
                Data = regions
            });
        }

        // =============================================
        // BÀI 3: Register và Login
        // =============================================

        // API Đăng ký tài khoản mới
        // POST: api/APIGame/Register
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            // Tạo đối tượng ApplicationUser từ dữ liệu DTO nhận được
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                RegionId = dto.RegionId,
                IsDeleted = false,
                RoleId = 2, // Mặc định là Player
                LinkAvatar = "default_avatar.png",
                OTP = "" // Sửa lỗi Null constraint trên database
            };

            // UserManager.CreateAsync tự động HASH mật khẩu và lưu DB
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = "Đăng ký thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    Data = null
                });
            }

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = $"Đăng ký tài khoản '{dto.UserName}' thành công!",
                Data = new { UserId = user.Id, UserName = user.UserName }
            });
        }

        // API Đăng nhập
        // POST: api/APIGame/Login
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (!result.Succeeded)
                return Unauthorized(new ResponseApi { IsSuccess = false, Notification = "Tên đăng nhập hoặc mật khẩu không chính xác", Data = null });
            
            var user = await _userManager.FindByNameAsync(dto.UserName);
            
            // --- TẠO JWT TOKEN THEO LAB 5 ---
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, user!.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email ?? "")
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"]!)),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            // -------------------------------

            return Ok(new ResponseApi 
            { 
                IsSuccess = true, 
                Notification = "Đăng nhập thành công!", 
                Data = new 
                { 
                    Token = tokenString,
                    UserId = user.Id, 
                    user.UserName, 
                    user.Name 
                } 
            });
        }

        // =============================================
        // BÀI 4: GET với Path Parameter
        // =============================================

        // API lấy câu hỏi theo cấp độ (truyền levelId)
        // GET: api/APIGame/GetAllQuestionGameByLevel/1
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("GetAllQuestionGameByLevel/{levelId}")]
        public async Task<IActionResult> GetAllQuestionGameByLevel(int levelId)
        {
            var questions = await _context.Questions
                .Where(q => q.LevelId == levelId) // Lọc theo levelId được truyền vào
                .ToListAsync();

            if (!questions.Any())
            {
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = $"Không tìm thấy câu hỏi nào ở cấp độ {levelId}",
                    Data = null
                });
            }

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = $"Lấy câu hỏi cấp độ {levelId} thành công",
                Data = questions
            });
        }

        // =============================================
        // BÀI 5: POST lưu kết quả
        // =============================================

        // API lưu kết quả thi đấu của người dùng
        // POST: api/APIGame/SaveResult
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpPost("SaveResult")]
        public async Task<IActionResult> SaveResult([FromBody] LevelResultDTO dto)
        {
            // Kiểm tra user có tồn tại không
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = "Không tìm thấy người dùng",
                    Data = null
                });
            }

            var levelResult = new LevelResult
            {
                UserId = dto.UserId,
                LevelId = dto.LevelId,
                Score = dto.Score,
                CompletionDate = dto.CompletionDate == default ? System.DateTime.Now : dto.CompletionDate
            };

            _context.LevelResults.Add(levelResult);
            await _context.SaveChangesAsync();

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lưu kết quả thành công!",
                Data = levelResult
            });
        }

        // =============================================
        // BÀI 6: GET xếp hạng người chơi
        // =============================================

        // API xếp hạng người chơi theo tổng điểm
        // GET: api/APIGame/Rating
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("Rating")]
        public async Task<IActionResult> Rating()
        {
            // LINQ query phức tạp: Group kết quả theo User và tính tổng điểm
            var ratings = await _context.LevelResults
                .GroupBy(lr => lr.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    TotalScore = g.Sum(lr => lr.Score) // Tổng điểm của từng user
                })
                .OrderByDescending(x => x.TotalScore)  // Sắp xếp giảm dần
                .ToListAsync();

            // Kết hợp với thông tin user
            var result = new List<RatingVM>();
            int rank = 1;
            foreach (var r in ratings)
            {
                var user = await _userManager.FindByIdAsync(r.UserId);
                var region = await _context.Regions.FindAsync(user?.RegionId ?? 0);
                result.Add(new RatingVM
                {
                    UserId = r.UserId,
                    UserName = user?.UserName ?? "Unknown",
                    Name = user?.Name ?? "Unknown",
                    RegionName = region?.Name ?? "Không xác định",
                    TotalScore = r.TotalScore,
                    Rank = rank++
                });
            }

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lấy bảng xếp hạng thành công",
                Data = result
            });
        }

        // =============================================
        // BÀI 7: GET thông tin cá nhân user
        // =============================================

        // API lấy thông tin cá nhân của một user
        // GET: api/APIGame/GetUserInformation/{userId}
        [Microsoft.AspNetCore.Http.Tags("Lab 03 - API CRUD & Auth")]
        [HttpGet("GetUserInformation/{userId}")]
        public async Task<IActionResult> GetUserInformation(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = "Không tìm thấy người dùng",
                    Data = null
                });
            }

            var region = await _context.Regions.FindAsync(user.RegionId);
            var gameRole = await _context.GameRoles.FindAsync(user.RoleId);

            var vm = new UserInformationVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                RegionName = region?.Name ?? "Không xác định",
                LinkAvatar = user.LinkAvatar,
                RoleName = gameRole?.Name ?? "Player"
            };

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Lấy thông tin người dùng thành công",
                Data = vm
            });
        }
        // =============================================
        // LAB 4 - BÀI 1: PUT Đổi mật khẩu
        // =============================================

        // API đổi mật khẩu - cần xác nhận mật khẩu cũ trước
        // PUT: api/APIGame/ChangeUserPassword
        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpPut("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy người dùng", Data = null });

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = "Đổi mật khẩu thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    Data = null
                });

            return Ok(new ResponseApi { IsSuccess = true, Notification = "Đổi mật khẩu thành công!", Data = null });
        }

        // =============================================
        // LAB 5 - BÀI 3: GET Kết quả theo User (Bảo mật JWT)
        // =============================================

        // API lấy toàn bộ kết quả của người dùng
        // GET: api/APIGame/GetAllResultByUser/{userId}
        // Thêm Authorize để yêu cầu Token (Lab 5)
        [Microsoft.AspNetCore.Http.Tags("Lab 05 - Security (JWT Auth)")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet("GetAllResultByUser/{userId}")]
        public async Task<IActionResult> GetAllResultByUser(string userId)
        {
            var results = await _context.LevelResults
                .Where(lr => lr.UserId == userId)
                .Include(lr => lr.GameLevel)
                .ToListAsync();

            if (!results.Any())
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy kết quả nào", Data = null });

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = $"Lấy danh sách kết quả của User {userId} thành công",
                Data = results
            });
        }

        // =============================================
        // LAB 4 - BÀI 2: PUT Cập nhật thông tin (kèm Upload Avatar)
        // =============================================

        // API cập nhật thông tin cá nhân và upload ảnh đại diện
        // PUT: api/APIGame/UpdateUserInformation (dùng form-data)
        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpPut("UpdateUserInformation")]
        public async Task<IActionResult> UpdateUserInformation([FromForm] UpdateUserInformationDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy người dùng", Data = null });

            user.Name = dto.Name;
            user.RegionId = dto.RegionId;

            if (dto.AvatarFile != null && dto.AvatarFile.Length > 0)
            {
                var fileName = $"{user.Id}_{System.DateTime.Now.Ticks}{Path.GetExtension(dto.AvatarFile.FileName)}";
                var uploadPath = Path.Combine(_env.WebRootPath, "avatars", fileName);

                using var stream = new FileStream(uploadPath, FileMode.Create);
                await dto.AvatarFile.CopyToAsync(stream);

                user.LinkAvatar = $"/avatars/{fileName}";
            }

            await _userManager.UpdateAsync(user);

            return Ok(new ResponseApi
            {
                IsSuccess = true,
                Notification = "Cập nhật thông tin thành công!",
                Data = new { user.Name, user.RegionId, user.LinkAvatar }
            });
        }

        // =============================================
        // LAB 4 - BÀI 3: DELETE Xóa tài khoản (Soft Delete)
        // =============================================

        // API xóa mềm tài khoản
        // DELETE: api/APIGame/DeleteAccount/{userId}
        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy người dùng", Data = null });

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            return Ok(new ResponseApi { IsSuccess = true, Notification = "Đã vô hiệu hóa tài khoản thành công!", Data = null });
        }

        // =============================================
        // LAB 4 - BÀI 5: Quên mật khẩu
        // =============================================

        // API quên mật khẩu: tạo OTP ngẫu nhiên và gửi email
        // POST: api/APIGame/ForgotPassword
        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy tài khoản với email này", Data = null });

            var otp = new System.Random().Next(100000, 999999).ToString();
            user.OTP = otp;
            await _userManager.UpdateAsync(user);

            string htmlBody = $@"
                <div style='font-family: Arial; padding: 20px;'>
                    <h2>🔐 Khôi phục mật khẩu Game106</h2>
                    <p>Xin chào <b>{user.Name}</b>,</p>
                    <p>Mã OTP của bạn là:</p>
                    <h1 style='color: #e74c3c; letter-spacing: 8px;'>{otp}</h1>
                    <p>Mã có hiệu lực trong 5 phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                </div>";

            await _emailService.SendEmailAsync(email, "[Game106] Mã OTP khôi phục mật khẩu", htmlBody);

            return Ok(new ResponseApi { IsSuccess = true, Notification = $"Mã OTP đã được gửi đến {email}", Data = null });
        }

        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpPost("CheckOTP")]
        public async Task<IActionResult> CheckOTP([FromBody] CheckOTPDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy tài khoản", Data = null });

            if (user.OTP != dto.OTP)
                return BadRequest(new ResponseApi { IsSuccess = false, Notification = "Mã OTP không chính xác", Data = null });

            return Ok(new ResponseApi { IsSuccess = true, Notification = "Xác thực OTP thành công!", Data = null });
        }

        [Microsoft.AspNetCore.Http.Tags("Lab 04 - Advanced API & Mail")]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound(new ResponseApi { IsSuccess = false, Notification = "Không tìm thấy tài khoản", Data = null });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!result.Succeeded)
                return BadRequest(new ResponseApi
                {
                    IsSuccess = false,
                    Notification = "Đặt lại mật khẩu thất bại: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    Data = null
                });

            user.OTP = null;
            await _userManager.UpdateAsync(user);

            return Ok(new ResponseApi { IsSuccess = true, Notification = "Đặt lại mật khẩu thành công!", Data = null });
        }
    }
}
