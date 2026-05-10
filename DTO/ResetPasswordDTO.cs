namespace Game106.Backend.DTO
{
    // DTO để reset mật khẩu sau khi xác thực OTP thành công
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
