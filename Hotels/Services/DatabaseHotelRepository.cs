using Hotels.Data;
using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task GetOneByIdAsync(long id);
    }
    public class DatabaseHotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetOneByIdAsync(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel;
        }
    }
}
