using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Amenity
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}
