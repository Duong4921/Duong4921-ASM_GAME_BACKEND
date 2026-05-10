using System.ComponentModel.DataAnnotations;

namespace Game106.Backend.Models
{
    // Bảng quản lý Cấp độ Game (GameLevel)
    // Mỗi cấp độ chứa nhiều câu hỏi (Question)
    public class GameLevel
    {
        [Key]
        public int LevelId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
