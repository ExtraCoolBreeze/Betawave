/*Project name: Betawave
Author: Craig McMillan
Date: 06/05/2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description:  This is Account class for storing account details */

namespace Betawave.Classes
{
    public class Account
    {
        //declaring variables
        private int pkaccount_id;
        private string username;
        private string userpassword;
        private bool isadmin;

        /// <summary>
        /// The account constructor
        /// </summary>
        public Account()
        {
            pkaccount_id = 0;
            username = "";
            userpassword = "";
            isadmin = false;
        }

        /// <summary>
        /// Returns the username variable of account
        /// </summary>
        /// <returns></returns>
        public string GetUsername()
        {
            return username;
        }

        /// <summary>
        /// Set the username variable based on input
        /// </summary>
        /// <param name="username"></param>
        public void SetUsername(string username)
        {
           this.username = username;
        }

        /// <summary>
        /// When passed a viable password, this function will check password for valid parameters and once complete set the password variable 
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            // calling checking password function to validate security
            if (IsValidPassword(password) == "pass")
            {
                this.userpassword = password;
            }
        }

        /// <summary>
        /// Sets the accountId variable based on input
        /// </summary>
        /// <param name="data"></param>
        public void SetAccountId(int data)
        {
            pkaccount_id = data;
        }

        /// <summary>
        /// Returns the accountId variable
        /// </summary>
        /// <returns></returns>
        public int GetAccountId()
        { 
            return pkaccount_id;
        }

        /// <summary>
        /// Returns the userpassword variable
        /// </summary>
        /// <returns></returns>
        public string GetPassword()
        {
            return userpassword;
        }

        /// <summary>
        /// Sets the isadmin variable based on input
        /// </summary>
        /// <param name="data"></param>
        public void SetIsAdmin(bool data)
        { 
            isadmin = data;
        }

        /// <summary>
        /// Returns the isadmin variable
        /// </summary>
        /// <returns></returns>
        public bool GetIsAdmin()
        { 
            return isadmin;
        }

        /// <summary>
        /// prints the contents of the account class for testing
        /// </summary>
        public void PrintAccountDetails()
        {
            Console.WriteLine("Username: " + GetUsername());
            Console.WriteLine("Password: " + GetPassword());
            Console.WriteLine("Username: " + GetAccountId());
            Console.WriteLine("Password: " + GetIsAdmin());
        }

        /// <summary>
        /// Accepts a password. Checks if the password meets basic security targets. 
        /// At least 8 characters, cannot be null or white space, and includes a number, as well as an upper case and lower case character.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string IsValidPassword(string password)
        {
            string hasUpperChar = "fail";
            string hasLowerChar = "fail";
            string hasNumber = "fail";
            string isLongEnough = "fail";
            string notEmpty = "fail";



            // Check if viable password is not null or whitespace
            if (!string.IsNullOrWhiteSpace(password))
            {
                notEmpty = "pass";

                // Check if password is at least 8 characters long
                if (password.Length >= 8)
                {
                    isLongEnough = "pass";
                }

                // Checks if the password has at least 1 upper, 1 lower and 1 digit
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
                }
            }

            //Checks if the password passes all checks 
            if (notEmpty == "pass" && isLongEnough == "pass" && hasUpperChar == "pass" && hasLowerChar == "pass" && hasNumber == "pass")
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