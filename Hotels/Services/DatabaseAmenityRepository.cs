﻿using Hotels.Data;
using Hotels.Models;
using Hotels.Models.Api;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class DatabaseAmenityRepository : IAmenityRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseAmenityRepository(HotelDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AmenityDto> GetAllAsync()
        {
            return _context.Amenities
                .Select(amenity => new AmenityDto
                {
                    Id = amenity.Id,
                    Name = amenity.Name
                })
                .ToList();
        }

        public AmenityDto GetOneByIdAsync(long id)
        {
            return _context.Amenities
                .Select(amenity => new AmenityDto
                {
                    Id = amenity.Id,
                    Name = amenity.Name
                })
                .FirstOrDefault(a => a.Id == id);
        }

        public async Task CreateAsync(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<Amenity> DeleteAsync(long id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
            {
                return null;
            }

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task<bool> UpdateAsync(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AmenityExists(amenity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private async Task<bool> AmenityExists(long id)
        {
            return await _context.Amenities.AnyAsync(e => e.Id == id);
        }
    }
}
