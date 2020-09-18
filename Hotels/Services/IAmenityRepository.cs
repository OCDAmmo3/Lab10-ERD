using Hotels.Models;
using Hotels.Models.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IAmenityRepository
    {
        IEnumerable<AmenityDto> GetAllAsync();
        AmenityDto GetOneByIdAsync(long id);
        Task CreateAsync(Amenity amenity);
        Task<Amenity> DeleteAsync(long id);
        Task<bool> UpdateAsync(Amenity amenity);
    }
}
