using Hotels.Data;
using Hotels.Models;
using Hotels.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hotels.Tests.Services
{
    public class DatabaseTestBase : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly HotelDbContext _db;

        public DatabaseTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new HotelDbContext(
                new DbContextOptionsBuilder<HotelDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room { Name = "Studio Room", Layout = Room.RoomLayout.Studio };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.Id);
            return room;
        }

        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity { Name = "Jacuzzi" };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, amenity.Id);
            return amenity;
        }

        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel { Name = "Marriot", City = "Cedar Rapids", State = "IA", StreetAddress = "1234 5th St" };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id);
            return hotel;
        }
    }
}