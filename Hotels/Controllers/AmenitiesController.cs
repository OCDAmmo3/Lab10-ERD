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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityRepository repository;

        public AmenitiesController(IAmenityRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Amenities
        [HttpGet]
        public IEnumerable<AmenityDto> GetAmenities()
        {
            return repository.GetAllAsync();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public ActionResult<AmenityDto> GetAmenity(long id)
        {
            var amenity = repository.GetOneByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(long id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(amenity);

            if (!didUpdate)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Amenities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            await repository.CreateAsync(amenity);

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(long id)
        {
            Amenity amenity = await repository.DeleteAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }
    }
}
