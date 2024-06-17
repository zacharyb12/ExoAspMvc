using System.ComponentModel.DataAnnotations;

namespace ExoAspMvc.Models
{
    public class CreateContact
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
