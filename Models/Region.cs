using System.ComponentModel.DataAnnotations;

namespace Game106.Backend.Models
{
    // Bảng quản lý Vùng thi đấu của người dùng (Region)
    // Một Region có thể chứa nhiều User
    public class Region
    {
        [Key]
        public int RegionId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
