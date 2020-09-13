namespace Hotels.Models
{
    public class HotelRoom
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public long RoomId { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }

        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
    }
}
