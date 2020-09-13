using Hotels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity> GetOneByIdAsync(long id);
        Task CreateAsync(Amenity amenity);
        Task<Amenity> DeleteAsync(long id);
        Task<bool> UpdateAsync(Amenity amenity);
    }
}
