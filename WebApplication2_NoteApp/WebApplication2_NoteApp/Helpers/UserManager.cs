using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace WebApplication2_NoteApp.Helpers
{
    public class UserManager
    {
        private SqlConnection connection;
        private SqlCommand command;

        public UserManager()
        {
            connection = new SqlConnection("Server=.;Database=Z36NoteAppDB;Trusted_Connection=true");
            command = connection.CreateCommand();
        }

        public bool AddUser(string username, string password)
        {
            command.CommandText = "INSERT INTO Users(Name,Surname,Username,Password) VALUES(NULL,NULL,@username,@password)";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return result > 0;
        }

        public User Authenticate(string username, string password)
        {
            command.CommandText = "SELECT Id,Name,Surname,Username FROM Users WHERE Username=@username AND Password=@password";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            User user = null;

            while (reader.Read())
            {
                user = new User();
                user.Id = reader.GetInt32("Id");
                user.Name = reader.IsDBNull("Name") ? "" : reader.GetString("Name");
                user.Surname = reader.IsDBNull("Surname") ? "" : reader.GetString("Surname");
                user.Username = reader.GetString("Username");
            }

            connection.Close();

            return user;
        }

        public User GetUserById(int userId)
        {
            command.CommandText = "SELECT Id,Name,Surname,Username FROM Users WHERE Id = @userid";
            command.Parameters.AddWithValue("@userid", userId);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            User user = null;

            while (reader.Read())
            {
                user = new User();
                user.Id = reader.GetInt32("Id");
                user.Name = reader.IsDBNull("Name") ? "" : reader.GetString("Name");
                user.Surname = reader.IsDBNull("Surname") ? "" : reader.GetString("Surname");
                user.Username = reader.GetString("Username");
            }

            connection.Close();

            return user;
        }

        public bool UpdateProfile(int userId, string name, string surname)
        {
            command.CommandText = "UPDATE Users SET Name=@name,Surname=@surname WHERE Id=@id";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@id", userId);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return result > 0;
        }

        public bool UpdatePassword(int userId, string? password)
        {
            command.CommandText = "UPDATE Users SET Password=@password WHERE Id=@id";
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@id", userId);

            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return result > 0;
        }
    }
}
