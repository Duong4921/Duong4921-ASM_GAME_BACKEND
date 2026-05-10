using System;

namespace Game106.Backend.DTO
{
    // DTO để nhận dữ liệu kết quả thi đấu từ client
    public class LevelResultDTO
    {
        public string UserId { get; set; }
        public int LevelId { get; set; }
        public int Score { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
