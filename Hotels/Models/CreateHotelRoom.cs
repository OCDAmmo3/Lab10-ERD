using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Models
{
    public class CreateHotelRoom
    {
        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public long RoomId { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Rate { get; set; }
        [Required]
        public bool PetFriendly { get; set; }
    }
}
