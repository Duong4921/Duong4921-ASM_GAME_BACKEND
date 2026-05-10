using System.ComponentModel.DataAnnotations;

namespace Game106.Backend.Models
{
    // Bảng quản lý Vai trò (Role): phân biệt Admin và Player
    public class GameRole
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
