using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game106.Backend.Models
{
    [Table("Players")] // Yêu cầu hệ thống tạo bảng tên là "Players"
    public class Player
    {
        [Key] // Đánh dấu đây là Khóa Chính (Primary Key)
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên người chơi không được để trống")]
        [MaxLength(100)] // Chiều dài tối đa 100 ký tự
        public string Username { get; set; } = string.Empty;

        public int Score { get; set; }
    }
}
