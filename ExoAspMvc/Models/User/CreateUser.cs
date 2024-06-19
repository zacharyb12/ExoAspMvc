using System.ComponentModel.DataAnnotations;

namespace ExoAspMvc.Models.User
{
    public class CreateUser
    {
        [Required]
        [MinLength(2)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(2)]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Pseudo { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$", ErrorMessage = "Le mot de passe doit contenir 1 Maj, 1 Min, 1 chiffre, 1 char spécial")]
        public string PasswordCheck { get; set; }
    }
}
