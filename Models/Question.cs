using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game106.Backend.Models
{
    // Bảng câu hỏi trắc nghiệm (Question)
    // Mỗi câu hỏi thuộc về một cấp độ game (LevelId - khóa ngoại)
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public string ContentQuestion { get; set; }

        [Required]
        public string Answer { get; set; }       // Đáp án đúng

        public string Option1 { get; set; }      // Lựa chọn 1
        public string Option2 { get; set; }      // Lựa chọn 2
        public string Option3 { get; set; }      // Lựa chọn 3
        public string Option4 { get; set; }      // Lựa chọn 4

        // Khóa ngoại trỏ tới bảng GameLevel
        [ForeignKey("GameLevel")]
        public int LevelId { get; set; }

        // Navigation property - EF dùng để JOIN bảng
        public GameLevel GameLevel { get; set; }
    }
}
