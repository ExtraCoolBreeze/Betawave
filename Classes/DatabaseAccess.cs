﻿using MySql.Data.MySqlClient;
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

        public async Task<bool> CheckUserExists(string username)
        {
            using (var connection = new MySqlConnection(MySqlConnectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM account WHERE username = @Username", connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int userCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return userCount > 0;
                }
            }
        }

        public async Task CreateUser(string username, string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);
                using (var connection = new MySqlConnection(MySqlConnectionString))
                {
                    await connection.OpenAsync();
                    using (var cmd = new MySqlCommand("INSERT INTO account (username, userpassword) VALUES (@Username, @UserPassword);", connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@UserPassword", hashedPassword);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in CreateUser: {e.Message}");
                throw;  // Or handle the exception appropriately
            }
        }


        public async Task<bool> ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            using (var connection = new MySqlConnection(MySqlConnectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM account WHERE username = @Username AND userpassword = @UserPassword";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@UserPassword", hashedPassword);

                    int userCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
                        return true; // User is an admin
                    }
                }
            }
            return false; // Default to false if no result or the user is not an admin
        }

    }
}