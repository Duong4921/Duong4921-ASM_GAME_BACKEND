using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game106.Backend.Context;
using Game106.Backend.Models;

namespace Game106.Backend.Controllers
{
    [ApiController]             // Khai báo tư cách Controller của API
    [Route("api/[controller]")] // Quy định đường dẫn URL mẫu (Vd: /api/players)
    public class PlayersController : ControllerBase
    {
        private readonly GameDbContext _context;

        // 1. Tiêm (Inject) DbContext qua constructor để sử dụng cơ sở dữ liệu
        public PlayersController(GameDbContext context)
        {
            _context = context;
        }

        // 2. GET: Truy xuất toàn bộ danh sách người chơi
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Player>>>> GetPlayers()
        {
            // Bất đồng bộ: await lấy từ DB
            var players = await _context.Players.ToListAsync();
            
            return Ok(new ApiResponse<IEnumerable<Player>>
            {
                Success = true,
                Message = "Lấy dữ liệu toàn bộ người chơi thành công!",
                Data = players
            });
        }

        // 3. POST: Thêm mới người chơi vào Server
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Player>>> CreatePlayer([FromBody] Player newPlayer)
        {
            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync(); // Phát súng thứ hai quyết định ghi vào SQL

            return Ok(new ApiResponse<Player>
            {
                Success = true,
                Message = "Khởi tạo người chơi mới trót lọt!",
                Data = newPlayer // Trả ngược thông tin gồm cả ID do SQL mới sinh ra cho Unity dùng
            });
        }

        // 4. PUT: Chỉnh sửa toàn phần một người chơi
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Player>>> UpdatePlayer(int id, [FromBody] Player updatedPlayer)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound(new ApiResponse<Player> { Success = false, Message = "Lỗi: Không tìm thấy ID này" });
            }

            player.Username = updatedPlayer.Username;
            player.Score = updatedPlayer.Score;
            
            await _context.SaveChangesAsync();
            return Ok(new ApiResponse<Player> { Success = true, Message = "Cập nhật người chơi hoàn tất", Data = player });
        }

        // 5. PATCH: Hành động cập nhật nhanh một tham số (Điểm số)
        [HttpPatch("{id}/score")]
        public async Task<ActionResult<ApiResponse<Player>>> UpdateScore(int id, [FromBody] int score)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound(new ApiResponse<Player> { Success = false, Message = "Xịt! Người chơi không tồn tại" });

            player.Score = score; // Chỉ đổi điểm, giữ nguyên chức vụ
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<Player> { Success = true, Message = "Đã cập nhật điểm số thành công", Data = player });
        }

        // 6. DELETE: Bay màu 1 người chơi
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<string> { Success = true, Message = $"Đã tiễn người chơi số {id} về cát bụi!" });
        }
    }
}
