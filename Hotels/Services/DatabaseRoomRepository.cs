using Hotels.Data;
using Hotels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class DatabaseRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .Include(r => r.HotelRooms)
                .ThenInclude(hr => hr.Hotel)
                .ToListAsync();
        }

        public async Task<Room> GetOneByIdAsync(long id)
        {
            var room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task CreateAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteAsync(long id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return null;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists(room.Id))
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

        private async Task<bool> RoomExists(long id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }

        public async Task AddAmenityAsync(long amenityId, long roomId)
        {
            var roomAmenity = new RoomAmenity
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenityAsync(long amenityId, long roomId)
        {
            var roomAmenity = await _context.RoomAmenities.FindAsync(amenityId, roomId);

            _context.RoomAmenities.Remove(roomAmenity);
            await _context.SaveChangesAsync();
        }
    }
}
