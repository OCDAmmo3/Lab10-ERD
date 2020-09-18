using Hotels.Models;
using Hotels.Models.Api;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Register(RegisterData data);
    }
}
