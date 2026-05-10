namespace Game106.Backend.ViewModel
{
    // ViewModel chứa dữ liệu xếp hạng người chơi
    // ViewModel = "Tờ kết quả" trả về cho client, chỉ chứa thông tin cần thiết
    public class RatingVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public int TotalScore { get; set; }   // Tổng điểm tích lũy
        public int Rank { get; set; }          // Thứ hạng xếp hạng
    }
}
