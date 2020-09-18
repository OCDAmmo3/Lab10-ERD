using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Models.Api
{
    public class RoomDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Layout { get; set; }
        public List<AmenityDto> Amenities { get; set; }
    }
}
