using Hotels.Data;
using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class DatabaseHotelRoomRepository : IHotelRoomRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelRoom>> GetAllAsync()
        {
            return await _context.HotelRooms.ToListAsync();
        }

        public async Task<HotelRoom> GetOneByIdAsync(long id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            return hotelRoom;
        }

        public async Task CreateAsync(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> DeleteAsync(long id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);

            if (hotelRoom == null)
            {
                return null;
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<bool> UpdateAsync(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelRoomExists(hotelRoom.Id))
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

        private async Task<bool> HotelRoomExists(long id)
        {
            return await _context.HotelRooms.AnyAsync(e => e.Id == id);
        }
    }
}
