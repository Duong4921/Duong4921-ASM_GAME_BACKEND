using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game106.Backend.Models
{
    // Bảng lưu kết quả thi đấu của người dùng (LevelResult)
    // Kết nối với User (UserId) và cấp độ game (LevelId)
    public class LevelResult
    {
        [Key]
        public int QuizResultId { get; set; }

        // Khóa ngoại sang ApplicationUser (chuỗi string vì Identity dùng string GUID)
        [Required]
        public string UserId { get; set; }

        // Khóa ngoại sang GameLevel
        [ForeignKey("GameLevel")]
        public int LevelId { get; set; }

        public int Score { get; set; }

        public DateTime CompletionDate { get; set; }

        // Navigation properties
        public GameLevel GameLevel { get; set; }
        public ApplicationUser User { get; set; }
    }
}
