using Microsoft.EntityFrameworkCore;
using Game106.Backend.Models;

namespace Game106.Backend.Context
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        // Báo cho SQL Server biết bạn có một bảng tên là Players 
        public DbSet<Player> Players { get; set; }
    }
}
