using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExoAspMvc.Models
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }

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
