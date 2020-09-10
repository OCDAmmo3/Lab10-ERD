using Hotels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IHotelRepository
    {
    }
    public class DatabaseHotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }
    }
}
