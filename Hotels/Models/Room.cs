using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Room
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public RoomLayout Layout { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }

        public List<HotelRoom> HotelRooms { get; set; }

        public enum RoomLayout
        {
            Studio = 0,
            OneBedroom = 1,
            TwoBedroom = 2
        }
    }
}
