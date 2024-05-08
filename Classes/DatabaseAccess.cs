/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This class was created to manage the connection to the database and perform basic checks and validation*/

using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

namespace Betawave.Classes
{
    public class DatabaseAccess
    {
        private string MySqlConnectionString;


        //constructor and root connection string
        public DatabaseAccess()
        {
            MySqlConnectionString = "server=localhost;userid=root;password=;database=betawave";
        }


        /// <summary>
        /// Connect function that uses the MySqlConnectionString variable to attempt to connect to the database and open a connection
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// When passed a username this function checks if that username exists in the database
        /// it establishes a connection, searches the database and returns true or false if it was found
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfUserExists(string username)
        {
            using (var connection = new MySqlConnection(MySqlConnectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM account WHERE username = @Username", connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int userCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (userCount > 0) 
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                     
                }
            }
        }

        /// <summary>
        /// When passed a username and password, this function adds those variables to the account table in the database.
        /// It opens a connection and adds the the information, if it cannot the error is handled
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task CreateUser(string username, string password)
        {
            try
            {
                //string hashedPassword = HashPassword(password);
                using (var connection = new MySqlConnection(MySqlConnectionString))
                {
                    await connection.OpenAsync();
                    using (var cmd = new MySqlCommand("INSERT INTO account (username, userpassword) VALUES (@Username, @UserPassword);", connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@UserPassword", password);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in CreateUser: {e.Message}");
            }
        }

        /// <summary>
        /// When passed the username and password, this function checks if they exist in the database.
        /// It opens a connection, searches the database and returns true or false if found.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUser(string username, string password)
        {
            //string hashedPassword = HashPassword(password);
            using (var connection = new MySqlConnection(MySqlConnectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM account WHERE username = @Username AND userpassword = @UserPassword";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserPassword", password);

                    int userCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (userCount > 0)
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
        }

/*        private string HashPassword(string password)
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
*/

        /// <summary>
        /// When passed a username this function searches the database to check if that user is an admin.
        /// It opens a connection, checks the database for the admin role and then returns true or false if found
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsAdmin(string username)
        {
            using (var connection = ConnectToMySql())
            {
                // SQL query to check admin status by joining the account, account_role, and role tables
                string query = @" SELECT r.admin FROM role r INNER JOIN account_role ar ON r.role_id = ar.role_id INNER JOIN account a ON ar.account_id = a.account_id WHERE a.username = @Username";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    var result = cmd.ExecuteScalar(); // Returns the 'admin' field value, expected to be 0 or 1

                    // Check the result and return true if the user is an admin
                    if (result != null && Convert.ToInt32(result) == 1)
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            
        }


        /// <summary>
        /// When passed a username this function searches the database and returns the associated details.
        /// It opens a connection, searches the database and stores the account information of the user in an account object and returns the object
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Account> GetUserByUsername(string username)
        {
            using (var connection = ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT a.account_id, a.username, a.userpassword, r.admin AS is_admin FROM account a JOIN account_role ar ON a.account_id = ar.account_id JOIN role r ON ar.role_id = r.role_id WHERE a.username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        Account user = new Account();
                        user.SetAccountId(reader.GetInt32("account_id"));
                        user.SetUsername(reader.GetString("username"));
                        user.SetPassword(reader.GetString("userpassword"));
                        user.SetIsAdmin(reader.GetBoolean("is_admin"));
                        return user;
                    }
                }
            }
            return null;
        }

/*        private async void DisplayErrorToUser()
        {
            await DisplayAlert("Error", "An unexpected error occurred. Please contact support if the problem persists.", "OK");
        }

        private async Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }*/
    }
}