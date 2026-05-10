using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Game106.Backend.Models;

namespace Game106.Backend.Context
{
    // Đổi kế thừa từ DbContext sang IdentityDbContext<ApplicationUser>
    // Lệnh này tự động tạo ~10 bảng Identity (AspNetUsers, AspNetRoles...) khi migrate
    public class GameDbContext : IdentityDbContext<ApplicationUser>
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        // ===== CÁC BẢNG DỮ LIỆU GAME =====
        public DbSet<Region> Regions { get; set; }
        public DbSet<GameRole> GameRoles { get; set; }
        public DbSet<GameLevel> GameLevels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<LevelResult> LevelResults { get; set; }

        // Phương thức này chạy khi EF tạo database, dùng để nhét dữ liệu mẫu vào
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Bắt buộc gọi base khi dùng Identity

            // ===== DỮ LIỆU MẪU (SEED DATA) =====

            // Thêm 3 vùng thi đấu
            builder.Entity<Region>().HasData(
                new Region { RegionId = 1, Name = "Miền Bắc" },
                new Region { RegionId = 2, Name = "Miền Trung" },
                new Region { RegionId = 3, Name = "Miền Nam" }
            );

            // Thêm 2 vai trò
            builder.Entity<GameRole>().HasData(
                new GameRole { RoleId = 1, Name = "Admin" },
                new GameRole { RoleId = 2, Name = "Player" }
            );

            // Thêm 3 cấp độ game
            builder.Entity<GameLevel>().HasData(
                new GameLevel { LevelId = 1, Title = "Dễ", Description = "Câu hỏi cơ bản cho người mới bắt đầu" },
                new GameLevel { LevelId = 2, Title = "Trung bình", Description = "Câu hỏi mức độ trung bình" },
                new GameLevel { LevelId = 3, Title = "Khó", Description = "Câu hỏi thử thách dành cho chuyên gia" }
            );

            // Thêm câu hỏi mẫu cho từng cấp độ
            builder.Entity<Question>().HasData(
                // Level 1 - Dễ
                new Question { QuestionId = 1, LevelId = 1, ContentQuestion = "2 + 2 = ?", Answer = "4", Option1 = "3", Option2 = "4", Option3 = "5", Option4 = "6" },
                new Question { QuestionId = 2, LevelId = 1, ContentQuestion = "Thủ đô của Việt Nam là gì?", Answer = "Hà Nội", Option1 = "Hà Nội", Option2 = "TP.HCM", Option3 = "Đà Nẵng", Option4 = "Huế" },
                // Level 2 - Trung bình
                new Question { QuestionId = 3, LevelId = 2, ContentQuestion = "Ngôn ngữ lập trình nào tạo ra .NET?", Answer = "C#", Option1 = "Java", Option2 = "Python", Option3 = "C#", Option4 = "Go" },
                new Question { QuestionId = 4, LevelId = 2, ContentQuestion = "HTTP status 404 có nghĩa là gì?", Answer = "Not Found", Option1 = "OK", Option2 = "Not Found", Option3 = "Server Error", Option4 = "Unauthorized" },
                // Level 3 - Khó
                new Question { QuestionId = 5, LevelId = 3, ContentQuestion = "Design pattern nào dùng trong Dependency Injection?", Answer = "Inversion of Control", Option1 = "Singleton", Option2 = "Factory", Option3 = "Inversion of Control", Option4 = "Observer" }
            );

            // ===== THÊM 5 USER TEST (ID từ 1 - 5) =====
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<ApplicationUser>();
            
            for (int i = 1; i <= 5; i++)
            {
                var user = new ApplicationUser
                {
                    Id = i.ToString(),
                    Name = $"Người chơi số {i}",
                    UserName = $"user{i}",
                    NormalizedUserName = $"USER{i}",
                    Email = $"user{i}@gmail.com",
                    NormalizedEmail = $"USER{i}@GMAIL.COM",
                    EmailConfirmed = true,
                    RegionId = 1,
                    RoleId = 2, // Player
                    LinkAvatar = "https://cdn-icons-png.flaticon.com/512/149/149071.png",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    OTP = ""
                };
                user.PasswordHash = hasher.HashPassword(user, "123456");
                builder.Entity<ApplicationUser>().HasData(user);
            }
        }
    }
}
