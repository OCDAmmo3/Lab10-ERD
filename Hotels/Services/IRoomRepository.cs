﻿using Hotels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetOneByIdAsync(long id);
        Task CreateAsync(Room room);
        Task<Room> DeleteAsync(long id);
        Task<bool> UpdateAsync(Room room);
    }
}