using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Betawave.Classes
{
    public class Account
    {
        private int pkaccount_id;
        private string username;
        private string userpassword;


        public Account()
        {
            pkaccount_id = 0;
            username = "";
            userpassword = "";
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
                this.userpassword = password;
                return true;
            }
            else
            {
                passwordError = "The password must be at least 8 characters long, include both upper and lower case letters, a number, and a special character.";
                return false;
            }
        }

        // need to complete this function, has to save the AccountId from the database 
        public void SetAccountId(int id)
        {
            pkaccount_id = id;
        }

        public int GetAccountId()
        { 
            return pkaccount_id;
        }

        public string GetPassword()
        {
            return userpassword;
        }

        public void PrintAccountDetails()
        {
            Console.WriteLine("Username: " + GetUsername());
            Console.WriteLine("Password: " + GetPassword()); // Note: Printing passwords can be a security risk

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