namespace Game106.Backend.DTO
{
    // DTO để cập nhật thông tin cá nhân (dùng form-data để upload avatar)
    public class UpdateUserInformationDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public IFormFile? AvatarFile { get; set; }  // File ảnh đại diện (nullable)
    }
}
