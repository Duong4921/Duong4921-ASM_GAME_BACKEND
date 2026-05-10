namespace Game106.Backend.ViewModel
{
    // ViewModel trả về thông tin cá nhân user
    public class UserInformationVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegionName { get; set; }
        public string LinkAvatar { get; set; }
        public string RoleName { get; set; }
    }
}
