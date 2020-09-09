using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
