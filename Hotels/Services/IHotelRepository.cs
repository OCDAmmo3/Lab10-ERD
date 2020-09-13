using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        Task<Hotel> GetHotelRoomsById(long hotelId);
        Task AddRoomAsync(long hotelId, long roomId);
        Task DeleteRoomAsync(long hotelId, long roomId);
    }
}
