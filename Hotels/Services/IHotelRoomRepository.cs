using Hotels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IHotelRoomRepository
    {
        Task<IEnumerable<HotelRoom>> GetAllAsync();
        Task<HotelRoom> GetOneByIdAsync(long id);
        Task CreateAsync(HotelRoom hotelRoom);
        Task<HotelRoom> DeleteAsync(long id);
        Task<bool> UpdateAsync(HotelRoom hotelRoom);
    }
}
