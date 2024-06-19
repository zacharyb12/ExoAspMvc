using ExoAspMvc.Models.RoleEnum;
using ExoAspMvc.Models.User;
using ExoAspMvc.Repository.AuthRepo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExoAspMvc.Repository.AuthRepo
{
    }
    public class AuthServices : IAuthServices
    {
        private readonly SqlConnection _connection;

        public AuthServices(SqlConnection connection)
        {
            _connection = connection;
        }

        public User Register(CreateUser user)
        {
            try
            {

                using (SqlCommand cmd = new SqlCommand("[CreateUser]", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar).Value = user.Firstname;
                    cmd.Parameters.Add("@Lastname", SqlDbType.NVarChar).Value = user.Lastname;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                    cmd.Parameters.Add("@Pseudo", SqlDbType.NVarChar).Value = user.Pseudo;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;


                    _connection.Open();

                    int Id = (int)cmd.ExecuteScalar();

                    User u = GetById(Id);

                    _connection.Close();
                    return u ?? new User();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {

                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [User_]";
                    command.CommandType = CommandType.Text;

                    _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Lastname = Convert.ToString(reader["Lastname"]),
                                Firstname = Convert.ToString(reader["Firstname"]),
                                Email = Convert.ToString(reader["Email"]),
                                Pseudo = Convert.ToString(reader["Pseudo"]),
                            });
                        }
                    }
                }

                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving users: {ex.Message}");
                return new List<User>(); // Return an empty list on error
            }
            return users;
        }

    public User? GetById(int id)
    {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM User_ WHERE Id = @Id";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", id);
            _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                            Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                            Pseudo = reader.GetString(reader.GetOrdinal("Pseudo")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = (Role)reader.GetInt32(reader.GetOrdinal("Role"))
                        };
                    _connection.Close();
                        return user;
                    }
                    else
                    {
                        Console.WriteLine($"No user found with ID {id}");
                        return null;
                    }
                }
            }
        }
    




    public User Login(string email, string password)
        {
        using (SqlConnection connection = _connection)
        {

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Login";
                command.Parameters.AddWithValue("Email", email);
                command.Parameters.AddWithValue("Password", password);

                connection.Open();

                object result = command.ExecuteScalar();
                Console.WriteLine($"Result from ExecuteScalar: {result} (Type: {result?.GetType()})");

                int Id = (int)command.ExecuteScalar();

                command.CommandText = "SELECT * FROM User_ WHERE Id = @Id";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                            Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                            Pseudo = reader.GetString(reader.GetOrdinal("Pseudo")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Role = (Role)reader.GetInt32(reader.GetOrdinal("Role"))
                        };
                        _connection.Close();
                        return user;
                    }
                    else
                    {
                        return new User();
                    }
                }
            }
        }
        }
        
}
