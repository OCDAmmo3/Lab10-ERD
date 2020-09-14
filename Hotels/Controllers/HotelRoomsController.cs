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
            var hotel = await repository.GetHotelRoomsById(hotelId);
            return hotel;
        }

        /*
        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("")]
        public async Task<IActionResult> PutHotelRoom(long id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.HotelId)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(hotelRoom);

            if (!didUpdate)
            {
                return NotFound();
            }

            return NoContent();
        }
        */

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
