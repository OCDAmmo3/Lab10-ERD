﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotels.Data;
using Hotels.Models;
using Hotels.Services;
using Hotels.Models.Api;

namespace Hotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository repository;

        public HotelsController(IHotelRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Hotels
        [HttpGet]
        public IEnumerable<HotelDto> GetHotels()
        {
            return repository.GetAllAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public ActionResult<HotelDto> GetHotel(long id)
        {
            var hotel = repository.GetOneByIdAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(long id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(hotel);

            if (!didUpdate)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await repository.CreateAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(long id)
        {
            Hotel hotel = await repository.DeleteAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
    }
}
