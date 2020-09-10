using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotels.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public RoomLayout Layout { get; set; }

        public enum RoomLayout
        {
            Studio = 0,
            OneBedroom = 1,
            TwoBedroom = 2
        }
    }
}
