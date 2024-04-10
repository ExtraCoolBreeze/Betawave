using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Data;

namespace Betawave.Classes
{
    public class DatabaseAccess
    {
        private string MySqlConnectionString = "server=localhost;userid=root;password=;database=betawave";

        public MySqlConnection ConnectToMySql()
        {
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            try
            {
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
                mySqlConnection?.Dispose();
                throw;
            }
        }

        public bool CheckUserExists(string username)
        {
            using (var connection = ConnectToMySql())
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM account WHERE username = @Username", connection))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0;
            }
        }

        public void CreateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = ConnectToMySql())
            using (var cmd = new MySqlCommand("INSERT INTO account (username, userpassword) VALUES (@Username, @UserPassword);", connection))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UserPassword", hashedPassword);
                cmd.ExecuteNonQuery();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string hashedPassword = HashPassword(password);

            // Use ConnectToMySql method to establish the connection
            using (var connection = ConnectToMySql())
            {
                // Use a parameterized query to prevent SQL injection
                string query = "SELECT COUNT(*) FROM account WHERE username = @Username AND userpassword = @UserPassword";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserPassword", hashedPassword);

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                    return userCount > 0;
                }
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            DataTable dataTable = new DataTable();

            using (var connection = ConnectToMySql())
            using (var cmd = new MySqlCommand(query, connection))
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

    }
}