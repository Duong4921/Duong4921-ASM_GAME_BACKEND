using Microsoft.AspNetCore.Identity;

namespace Game106.Backend.Models
{
    // ApplicationUser kế thừa IdentityUser của Microsoft
    // IdentityUser đã có sẵn: UserName, Email, PasswordHash, PhoneNumber...
    // Ta chỉ cần THÊM các trường tùy biến riêng của game
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }          // Tên hiển thị
        public int RegionId { get; set; }         // Vùng thi đấu
        public string LinkAvatar { get; set; }    // URL ảnh đại diện
        public bool IsDeleted { get; set; }       // Trạng thái xóa mềm
        public int RoleId { get; set; }           // Phân quyền
        public string OTP { get; set; }           // Mã OTP để xác thực
    }
}
