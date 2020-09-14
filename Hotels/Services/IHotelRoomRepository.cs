using Hotels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IHotelRoomRepository
    {
        Task<IEnumerable<HotelRoom>> GetHotelRoomsById(long hotelId);
        Task AddRoomAsync(long hotelId, CreateHotelRoom hotelRoom);
        Task DeleteRoomAsync(long hotelId, int roomNumber);
        Task<bool> UpdateAsync(HotelRoom hotelRoom);
        Task<HotelRoom> GetHotelRoomAsync(long hotelId, int roomNumber);
    }
}
