/*Project name: Betawave
Author: Craig McMillan
Date: 06/05/2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description:  This is Account class for storing account details */

namespace Betawave.Classes
{
    public class Account
    {
        private int pkaccount_id;
        private string username;
        private string userpassword;
        private bool IsAdmin;


        public Account()
        {
            pkaccount_id = 0;
            username = "";
            userpassword = "";
            IsAdmin = false;
        }

        public string GetUsername()
        {
            return username;
        }

        public bool SetUsername(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                this.username = username;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetPassword(string password)
        {
            if (IsValidPassword(password) == "pass")
            {
                this.userpassword = password;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetAccountId(int data)
        {
            pkaccount_id = data;
        }

        public int GetAccountId()
        { 
            return pkaccount_id;
        }

        public string GetPassword()
        {
            return userpassword;
        }

        public void SetIsAdmin(bool data)
        { 
            IsAdmin = data;
        }

        public bool GetIsAdmin()
        { 
            return IsAdmin;
        }

        public void PrintAccountDetails()
        {
            Console.WriteLine("Username: " + GetUsername());
            Console.WriteLine("Password: " + GetPassword());
            Console.WriteLine("Username: " + GetAccountId());
            Console.WriteLine("Password: " + GetIsAdmin());


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