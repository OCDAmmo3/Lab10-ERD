using Hotels.Models;
using Hotels.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotels.Tests.Services
{
    public class DatabaseHotelRepositoryTests : DatabaseTestBase
    {
        [Fact]
        public async Task Can_add_and_remove_amenity_to_room()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();
            var amenity = await CreateAndSaveTestAmenity();

            var repository = new DatabaseRoomRepository(_db);

            // Act
            await repository.AddAmenityAsync(
                amenityId: amenity.Id,
                roomId: room.Id
                );

            // Assert
            var realRoom = await repository.GetOneByIdAsync(room.Id);
            Assert.Contains(realRoom.Amenities, ra => ra.Name == "Jacuzzi");

            // Act
            await repository.DeleteAmenityAsync(
                amenityId: amenity.Id,
                roomId: room.Id);

            // Assert
            realRoom = await repository.GetOneByIdAsync(room.Id);
            Assert.DoesNotContain(realRoom.Amenities, ra => ra.Name == "Jacuzzi");
        }

        [Fact]
        public async Task Can_add_and_remove_room_to_hotel()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();
            var hotel = await CreateAndSaveTestHotel();
            var createRoom = new CreateHotelRoom() { RoomNumber = 14, Rate = 149.99m, PetFriendly = true, RoomId = room.Id };

            var repository = new DatabaseHotelRoomRepository(_db);

            // Act
            await repository.AddRoomAsync(
                hotelId: hotel.Id,
                hotelRoom: createRoom
                );

            // Assert
            var realHotelRoom = await repository.GetHotelRoomAsync(hotel.Id, createRoom.RoomNumber);
            Assert.Equal(14, realHotelRoom.RoomNumber);
            Assert.True(realHotelRoom.PetFriendly);

            // Act
            await repository.DeleteRoomAsync(
                hotelId: hotel.Id,
                roomNumber: createRoom.RoomNumber
                );

            // Assert
            realHotelRoom = await repository.GetHotelRoomAsync(hotel.Id, createRoom.RoomNumber);
            Assert.Null(realHotelRoom);
        }
    }
}
