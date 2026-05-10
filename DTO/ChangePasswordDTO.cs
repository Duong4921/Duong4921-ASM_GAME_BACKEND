namespace Game106.Backend.DTO
{
    // DTO để đổi mật khẩu - cần xác nhận mật khẩu cũ trước khi đổi
    public class ChangePasswordDTO
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
