using Hotels.Models;
using Hotels.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IHotelRepository
    {
        IEnumerable<HotelDto> GetAllAsync();
        HotelDto GetOneByIdAsync(long id);
        Task<bool> UpdateAsync(Hotel hotel);
        Task CreateAsync(Hotel hotel);
        Task<Hotel> DeleteAsync(long id);
    }
}
