using MySql.Data.MySqlClient;  // Assume this is where your Account class and others are defined
using Betawave.Classes;

public class DatabaseManager
{
    private DatabaseAccess dbAccess;
    public Account CurrentUser { get; private set; } 

    public int CurrentUserRole { get; private set; }

    public DatabaseManager(DatabaseAccess access)
    {
        dbAccess = access;
        CurrentUser = new Account();
    }

    // Load user data at login
    public bool ReadInUserAccountTable(string username, string password)
    {
        if (dbAccess.ValidateUser(username, password))
        {
            using (var connection = dbAccess.ConnectToMySql())
            {
                var command = new MySqlCommand("SELECT * FROM account WHERE username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        CurrentUser = new Account();
                        string usernameError, passwordError;
                        CurrentUser.SetAccountId(reader.GetInt32("account_id"));
                        CurrentUser.SetUsername(reader.GetString("username"), out usernameError);
                        CurrentUser.SetPassword(reader.GetString("userpassword"), out passwordError);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public List<Account_Role> ReadRolesForAccount(int accountId)
    {
        var roles = new List<Account_Role>();
        using (var connection = dbAccess.ConnectToMySql())
        {
            var command = new MySqlCommand("SELECT fkaccount_id, fkrole_id FROM account_role WHERE fkaccount_id = @AccountId", connection);
            command.Parameters.AddWithValue("@AccountId", accountId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var accountRole = new Account_Role();
                    accountRole.SetAccountRole(reader.GetInt32("fkaccount_id"));
                    accountRole.SetRoleId(reader.GetInt32("fkrole_id"));
                    roles.Add(accountRole);
                }
            }
        }
        return roles;
    }



}


