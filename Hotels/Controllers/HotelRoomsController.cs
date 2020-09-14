using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotels.Models;
using Hotels.Services;

namespace Hotels.Controllers
{
    [Route("api/Hotels/{hotelId}")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomRepository repository;

        public HotelRoomsController(IHotelRoomRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Hotels/5/Rooms
        [HttpGet("Rooms")]
        public async Task<IEnumerable<HotelRoom>> GetHotelRooms(long hotelId)
        {
            var hotelRooms = await repository.GetHotelRoomsById(hotelId);
            return hotelRooms;
        }

        // GET: api/Hotels/5/Rooms/14
        [HttpGet("Rooms/{roomNumber}")]
        public async Task<HotelRoom> GetHotelRoomDetails(long hotelId, int roomNumber)
        {
            var hotelRoom = await repository.GetHotelRoomAsync(hotelId, roomNumber);
            return hotelRoom;
        }

        // POST: api/Hotels/5/Rooms
        [HttpPost("Rooms")]
        public async Task<ActionResult> AddRoom(long hotelId, CreateHotelRoom hotelRoom)
        {
            await repository.AddRoomAsync(hotelId, hotelRoom);
            return CreatedAtAction(nameof(AddRoom), new { hotelId, hotelRoom.RoomNumber }, null);
        }

        // DELETE: api/Hotels/5/Rooms/12
        [HttpDelete("Rooms/{roomNumber}")]
        public async Task<ActionResult> DeleteRoom(long hotelId, int roomNumber)
        {
            await repository.DeleteRoomAsync(hotelId, roomNumber);
            return Ok();
        }
    }
}
