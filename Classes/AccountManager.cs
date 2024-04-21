using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    public class AccountManager
    {
        private List<Account> accounts = new List<Account>();
        private DatabaseAccess dbAccess;

        public AccountManager(DatabaseAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        public async Task LoadAccountsAsync()
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT account_id, username, userpassword, IsAdmin FROM account", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var account = new Account();
                        account.SetAccountId(reader.GetInt32("account_id"));
                        account.SetUsername(reader.GetString("username"), out string usernameError);
                        account.SetPassword(reader.GetString("userpassword"), out string passwordError);
                        account.SetIsAdmin(reader.GetBoolean("IsAdmin"));
                        accounts.Add(account);
                    }
                }
            }
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("INSERT INTO account (username, userpassword, IsAdmin) VALUES (@Username, @UserPassword, @IsAdmin)", connection);
                command.Parameters.AddWithValue("@Username", account.GetUsername());
                command.Parameters.AddWithValue("@UserPassword", account.GetPassword());
                command.Parameters.AddWithValue("@IsAdmin", account.GetIsAdmin());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAccount(Account account)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].GetAccountId() == account.GetAccountId())
                {
                    accounts[i].SetUsername(account.GetUsername(), out string _);
                    accounts[i].SetPassword(account.GetPassword(), out string _);
                    accounts[i].SetIsAdmin(account.GetIsAdmin());
                    break;
                }
            }

            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("UPDATE account SET username = @Username, userpassword = @UserPassword, IsAdmin = @IsAdmin WHERE account_id = @AccountId", connection);
                command.Parameters.AddWithValue("@AccountId", account.GetAccountId());
                command.Parameters.AddWithValue("@Username", account.GetUsername());
                command.Parameters.AddWithValue("@UserPassword", account.GetPassword());
                command.Parameters.AddWithValue("@IsAdmin", account.GetIsAdmin());
                command.ExecuteNonQuery();
            }
        }

        public List<Account> GetAllAccounts()
        {
            return accounts;
        }

        public Account GetAccountById(int accountId)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].GetAccountId() == accountId)
                {
                    return accounts[i];
                }
            }
            return null;  // Return null if no account is found
        }
    }
}
