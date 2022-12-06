using System.Data.SqlClient;

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
    }
}
