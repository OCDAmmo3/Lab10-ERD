using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Models.Api
{
    public class HotelRoomDto
    {
        public long HotelId { get; set; }
        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
        public long RoomId { get; set; }
        public RoomDto Room { get; set; }
    }
}
