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
        Task<Hotel> GetOneByIdAsync(long id);
        Task<bool> UpdateAsync(Hotel hotel);
        Task CreateAsync(Hotel hotel);
        Task<Hotel> DeleteAsync(long id);
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

        public async Task CreateAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> DeleteAsync(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return null;
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<bool> UpdateAsync(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(hotel.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private async Task<bool> HotelExists(long id)
        {
            return await _context.Hotels.AnyAsync(e => e.Id == id);
        }
    }
}
