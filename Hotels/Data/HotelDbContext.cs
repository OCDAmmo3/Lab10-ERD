﻿using Hotels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Data
{
    public class HotelDbContext : IdentityDbContext<ApplicationUser>
    {
        public HotelDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasData(
                    new Room { Id = 1, Name = "Honeymoon Suite", Layout = Room.RoomLayout.Studio },
                    new Room { Id = 2, Name = "Single King", Layout = Room.RoomLayout.OneBedroom }
                );
            modelBuilder.Entity<Hotel>()
                .HasData(
                    new Hotel { Id = 1, Name = "Marriot CR", StreetAddress = "3350 26th Ave", City = "Cedar Rapids", State = "IA", Phone = "3193730354" }
                );
            modelBuilder.Entity<Amenity>()
                .HasData(
                    new Amenity { Id = 1, Name = "Mini Fridge" }
                );
            modelBuilder.Entity<RoomAmenity>()
                .HasKey(roomAmenity => new
                {
                    roomAmenity.AmenityId,
                    roomAmenity.RoomId,
                });
            modelBuilder.Entity<HotelRoom>()
                .HasKey(hotelRoom => new
                {
                    hotelRoom.HotelId,
                    hotelRoom.RoomNumber
                });
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
    }
}
