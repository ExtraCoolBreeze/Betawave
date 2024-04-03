using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betawave.Classes
{
    using System;

    public class Account
    {
        private int role_id;
        private string username;
        private string password;
        private int admin;

        public Account()
        {
            role_id = 1;
            username = "";
            password = "";
            admin = 0;
        }

        public string GetUsername()
        {
            return username;
        }

        public bool SetUsername(string username, out string usernameError)
        {
            usernameError = "";
            if (!string.IsNullOrWhiteSpace(username))
            {
                this.username = username;
                return true;
            }
            
            
                usernameError = "The username cannot be blank or contain spaces.";
                return false;
            
        }

        public bool SetPassword(string password, out string passwordError)
        {
            passwordError = "";
            if (IsValidPassword(password) == "pass")
            {
                this.password = password;
                return true;
            }
            else
            {
                passwordError = "The password must be at least 8 characters long, include both upper and lower case letters, a number, and a special character.";
                return false;
            }
        }


        public string GetPassword()
        {
            return password;
        }

        public void SetRoleId()
        {
            role_id += 1;
        }

        public int GetRoleId()
        {
            return role_id;
        }

        public void SetAdmin(int isAdmin)
        {
            admin = isAdmin;
        }

        public int GetAdmin()
        {
            return admin;
        }

        public void PrintAccountDetails()
        {
            Console.WriteLine("Username: " + GetUsername());
            Console.WriteLine("Password: " + GetPassword()); // Note: Printing passwords can be a security risk
            Console.WriteLine("Role ID: " + GetRoleId());
            Console.WriteLine("Admin: " + GetAdmin());
        }


        public string IsValidPassword(string password)
        {
            string hasUpperChar = "fail";
            string hasLowerChar = "fail";
            string hasNumber = "fail";
            string hasSpecialChar = "fail";
            string isLongEnough = "fail";
            string notEmpty = "fail";

            // Check if password is not null or whitespace
            if (!string.IsNullOrWhiteSpace(password))
            {
                notEmpty = "pass";

                // Check if the password length is at least 8 characters
                if (password.Length >= 8)
                {
                    isLongEnough = "pass";
                }

                // Iterate through each character in the password
                foreach (var charString in password)
                {
                    if (char.IsUpper(charString))
                    {
                        hasUpperChar = "pass";
                    }
                    else if (char.IsLower(charString))
                    {
                        hasLowerChar = "pass";
                    }
                    else if (char.IsDigit(charString))
                    {
                        hasNumber = "pass";
                    }
                    else if (!char.IsWhiteSpace(charString))
                    {
                        hasSpecialChar = "pass";
                    }    
                }
            }

            // Check all conditions for a valid password
            if (notEmpty == "pass" && isLongEnough == "pass" && hasUpperChar == "pass" && hasLowerChar == "pass" && hasNumber == "pass" && hasSpecialChar == "pass")
            {
                return "pass";
            }
            else
            {
                return "fail";
            }
        }
    }
}