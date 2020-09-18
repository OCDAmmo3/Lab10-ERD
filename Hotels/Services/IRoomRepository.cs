using Hotels.Models;
using Hotels.Models.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IRoomRepository
    {
        IEnumerable<RoomDto> GetAllAsync();
        RoomDto GetOneByIdAsync(long id);
        Task CreateAsync(Room room);
        Task<Room> DeleteAsync(long id);
        Task<bool> UpdateAsync(Room room);
        Task AddAmenityAsync(long amenityId, long roomId);
        Task DeleteAmenityAsync(long amenityId, long roomId);
    }
}
