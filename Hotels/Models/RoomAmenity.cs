namespace Hotels.Models
{
    public class RoomAmenity
    {
        public long AmenityId { get; set; }
        public long RoomId { get; set; }

        public Amenity Amenity { get; set; }
        public Room Room { get; set; }
    }
}