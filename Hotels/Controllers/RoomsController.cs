using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hotels.Models;
using Hotels.Services;
using Hotels.Models.Api;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository repository;

        public RoomsController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Rooms
        [HttpGet]
        public IEnumerable<RoomDto> GetRooms()
        {
            return repository.GetAllAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(long id)
        {
            var room = await repository.GetOneByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(long id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(room);

            if (!didUpdate)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await repository.CreateAsync(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(long id)
        {
            Room room = await repository.DeleteAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // POST: api/Rooms/5/Amenities/12
        [HttpPost("{roomId}/Amenities/{amenityId}")]
        public async Task<ActionResult> AddAmenity(long roomId, long amenityId)
        {
            await repository.AddAmenityAsync(amenityId, roomId);
            return CreatedAtAction(nameof(AddAmenity), new { amenityId, roomId }, null);
        }

        // DELETE: api/Rooms/5/Amenities/12
        [HttpDelete("{roomId}/Amenities/{amenityId}")]
        public async Task<ActionResult> DeleteAmenity(long roomId, long amenityId)
        {
            await repository.DeleteAmenityAsync(amenityId, roomId);
            return Ok();
        }
    }
}
