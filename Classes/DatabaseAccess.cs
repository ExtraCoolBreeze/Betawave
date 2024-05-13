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
    //declaring class
    public class DatabaseAccess
    {
        //declaring variable
        private string MySqlConnectionString;
        private ErrorLogger errorLogger;


        //constructor and root connection string and error log file location
        public DatabaseAccess()
        {
            MySqlConnectionString = "server=localhost;userid=root;password=;database=betawave";
            errorLogger = new ErrorLogger("C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\BetawaveErrorLog.txt");
        }


        /// <summary>
        /// Connect function that uses the MySqlConnectionString variable to attempt to connect to the database and open a connection
        /// </summary>
        /// <returns></returns>
        public MySqlConnection ConnectToMySql()
        {
            //connecting to database
            MySqlConnection mySqlConnection = new MySqlConnection(MySqlConnectionString);
            try
            {   //opening connection and returning connection
                mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception ex)
            {   //logging error and closing connection
                errorLogger.LogError(ex);
                mySqlConnection?.Dispose();
                return null;
            }
        }

        /// <summary>
        /// This function when called checks if there is a connection and then increments a count if successful and returns the count
        /// </summary>
        /// <returns></returns>
        public async Task<int> CheckDatabaseConnection()
        {
            int connectionSuccessCount = 0;
            MySqlConnection connection = null;

            try
            {   //trying to connect and opening connection and then returning value
                connection = new MySqlConnection(MySqlConnectionString);
                await connection.OpenAsync();
                connectionSuccessCount++;
            }
            catch (Exception ex)
            {   //logging error
                errorLogger.LogError(ex);
            }
            finally
            {   //closing connection
                connection?.Dispose(); 
            }
            //returning count
            return connectionSuccessCount;
        }




        /// <summary>
        /// When passed a username this function checks if that username exists in the database
        /// it establishes a connection, searches the database and returns true or false if it was found
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfUserExists(string username)
        {
            try
            {   //connecting to database and opening connection
                using (var connection = new MySqlConnection(MySqlConnectionString))
                {
                    await connection.OpenAsync();
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM account WHERE username = @Username", connection))
                    {   //checking is user exists and returning true or false based if anyting is found
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
            }   //catching potential and logging connection issues
            catch (Exception ex)
            {   
                errorLogger.LogError(ex);
                throw;
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
            {   //connecting to the database and inserting data
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
            }//catching error
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
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
            try
            {   //connecting to database and checking if username and password exist 
                using (var connection = new MySqlConnection(MySqlConnectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT COUNT(*) FROM account WHERE username = @Username AND userpassword = @UserPassword";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@UserPassword", password);
                        //if data is verified returns true if not false
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
            }   //catching exception
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// When passed a username this function searches the database to check if that user is an admin.
        /// It opens a connection, checks the database for the admin role and then returns true or false if found
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsAdmin(string username)
        {
            try
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
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
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
            try
            {   //connecting to database and getting user information based on username
                using (var connection = ConnectToMySql())
                {
                    var command = new MySqlCommand("SELECT a.account_id, a.username, a.userpassword, r.admin AS is_admin FROM account a JOIN account_role ar ON a.account_id = ar.account_id JOIN role r ON ar.role_id = r.role_id WHERE a.username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = await command.ExecuteReaderAsync())
                    {   //reading results into account object and returning object
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
            }   //catching errors and returning null
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                throw;
            }
            return null;
        }
    }
}