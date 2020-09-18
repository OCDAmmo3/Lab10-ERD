namespace Hotels.Models.Api
{
    public class AmenityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class RoomAmenityDto
    {
        public long RoomId { get; set; }
        public long AmenityId { get; set; }
        public Room Room { get; set; }
        public Amenity Amenity { get; set; }
    }
}
