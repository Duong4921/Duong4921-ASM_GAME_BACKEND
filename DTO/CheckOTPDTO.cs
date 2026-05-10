namespace Game106.Backend.DTO
{
    // DTO để kiểm tra OTP khi quên mật khẩu
    public class CheckOTPDTO
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
