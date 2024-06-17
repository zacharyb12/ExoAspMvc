using ExoAspMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExoAspMvc.Repository.ContactRepo
{
    public class ContactService : IContactService
    {

        private readonly SqlConnection _connection;

        public ContactService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Contact> GetContacts()
        {
                            List<Contact> contacts = new List<Contact>();
                try
                {

                        using (SqlCommand command = _connection.CreateCommand())
                        {
                            command.CommandText = "SELECT * FROM Contact";
                            command.CommandType = CommandType.Text;

                            _connection.Open();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                            contacts.Add(new Contact
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Lastname = Convert.ToString(reader["Lastname"]),
                                        Firstname = Convert.ToString(reader["Firstname"]),
                                        Password = Convert.ToString(reader["Password"]),
                                        Email = Convert.ToString(reader["Email"]),
                                    });
                                }
                            }
                        }
                    
                _connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while retrieving users: {ex.Message}");
                    return new List<Contact>(); // Return an empty list on error
                }
                            return contacts;

            }

        public Contact GetById(int id)
        {
            try
            {
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Lastname, Firstname, Email, Password FROM Contact WHERE Id = @Id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@Id", id);

                    _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Contact c = new Contact
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Lastname = Convert.ToString(reader["Lastname"]),
                                Firstname = Convert.ToString(reader["Firstname"]),
                                Password = Convert.ToString(reader["Password"]),
                                Email = Convert.ToString(reader["Email"])
                            };
                            return c;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving users: {ex.Message}");
                return null;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }


        public void AddContact(CreateContact contact)
        {
            using(SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Contact ( Email,Firstname,Lastname,Password ) VALUES ( @Email,@Firstname,@Lastname,@Password )";
                cmd.Parameters.AddWithValue("Email", contact.Email);
                cmd.Parameters.AddWithValue("Firstname", contact.Firstname);
                cmd.Parameters.AddWithValue("Lastname", contact.Lastname);
                cmd.Parameters.AddWithValue("Password", contact.Password);

                _connection.Open();

                int result = cmd.ExecuteNonQuery();

                _connection.Close();

            }
        }

        [HttpPost]
        public void Delete(int Id)
        {
            using( SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Contact WHERE Id  = @Id";

                cmd.Parameters.AddWithValue("Id", Id);

                _connection.Open();

                cmd.ExecuteNonQuery();

                _connection.Close();
            }
        }

        public void Update(Contact contact)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE Contact SET  Firstname = @Firstname,Lastname = @Lastname,Email = @Email,Password = @Password  WHERE Id  = @Id";

                cmd.Parameters.AddWithValue("Id", contact.Id);
                cmd.Parameters.AddWithValue("Lastname", contact.Lastname);
                cmd.Parameters.AddWithValue("Firstname", contact.Firstname);
                cmd.Parameters.AddWithValue("Email", contact.Email);
                cmd.Parameters.AddWithValue("Password", contact.Password);

                _connection.Open();

                cmd.ExecuteNonQuery();

                _connection.Close();
            }
        }
    }
}
