using System.ComponentModel.DataAnnotations;

namespace Hotels.Models.Api
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
