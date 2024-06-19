using ExoAspMvc.Models.RoleEnum;
namespace ExoAspMvc.Models.User
{
    public class User
    {

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Pseudo { get; set; }

        public  Role Role {get;set;}

    }
}
