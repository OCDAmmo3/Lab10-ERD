using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Api
{
    public class RegisterData
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
