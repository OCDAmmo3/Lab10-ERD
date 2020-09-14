using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class DatabaseHotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                .Include(h => h.HotelRooms)
                .ThenInclude(hr => hr.Room)
                .ToListAsync();
        }

        public async Task<Hotel> GetOneByIdAsync(long id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.HotelRooms)
                .ThenInclude(hr => hr.Room)
                .FirstOrDefaultAsync(h => h.Id == id);
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

        public async Task<Hotel> GetHotelRoomsById(long hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            return hotel;
        }

        public async Task AddRoomAsync(long hotelId, CreateHotelRoom hotelRoom)
        {
            var newHotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber
            };

            _context.HotelRooms.Add(newHotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(long hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();
        }
    }
}
