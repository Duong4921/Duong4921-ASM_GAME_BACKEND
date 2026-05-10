namespace Game106.Backend.DTO
{
    // DTO dùng để nhận dữ liệu đăng ký từ client gửi lên
    // DTO (Data Transfer Object) = "Tờ đơn" chứa thông tin cần thiết
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
    }
}
