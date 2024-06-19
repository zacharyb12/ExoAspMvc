using ExoAspMvc.Models.User;

namespace ExoAspMvc.Repository.AuthRepo
{
    public interface IAuthServices
    {
        public User Register(CreateUser user);

        public List<User> GetAll();

        public User GetById(int id);

        public User Login(string email, string password);
        
    }
}
