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
    public class DatabaseRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
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
    }
}
