using System.Collections.Generic;

namespace Hotels.Models.Api
{
    public class HotelDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public List<HotelRoomDto> Rooms { get; set; }
    }
}
