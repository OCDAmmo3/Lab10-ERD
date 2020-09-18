using Hotels.Data;
using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<HotelRoom>> GetHotelRoomsById(long hotelId)
        {
            var hotelRoom = await _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToListAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> GetHotelRoomAsync(long hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId)
                .Where(h => h.RoomNumber == roomNumber)
                .Include(hr => hr.Hotel)
                .ThenInclude(h => h.HotelRooms)
                .Include(hr => hr.Room)
                .ThenInclude(r => r.HotelRooms)
                .Include(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .ThenInclude(a => a.RoomAmenities)
                .FirstOrDefaultAsync();
            return hotelRoom;
        }

        public async Task AddRoomAsync(long hotelId, CreateHotelRoom hotelRoom)
        {
            var newHotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomId,
                PetFriendly = hotelRoom.PetFriendly,
                Rate = hotelRoom.Rate
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

        public async Task<bool> UpdateAsync(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(hotelRoom.HotelId))
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

        private async Task<bool> RoomExists(long hotelId)
        {
            return await _context.HotelRooms.AnyAsync(hr => hr.HotelId == hotelId);
        }
    }
}
