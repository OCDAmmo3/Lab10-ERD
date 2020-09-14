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

        public async Task<IEnumerable<HotelRoom>> GetAllAsync()
        {
            return await _context.HotelRooms.ToListAsync();
        }

        public async Task<IEnumerable<HotelRoom>> GetHotelRoomsById(long hotelId)
        {
            var hotelRoom = await _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToListAsync();
            return hotelRoom;
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

        public Task<bool> UpdateAsync(HotelRoom hotelRoom)
        {
            throw new System.NotImplementedException();
        }
    }
}
