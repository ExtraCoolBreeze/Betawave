/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created the interactions, updating and displaying of the UI on the CreateAccount content page */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes;
using MySql.Data.MySqlClient;

namespace Betawave.ViewModels
{   //inheriting for event updating
    public class CreateAccountViewModel : INotifyPropertyChanged
    {   //declaring variables
        private string username;
        private string password;
        private readonly DatabaseAccess dbAccess = new DatabaseAccess();
        //declaring event
        public event PropertyChangedEventHandler PropertyChanged;

        //class constructor
        public CreateAccountViewModel()
        {   
            //creating command that links to the button ui to call the create account method
            CreateAccountCommand = new Command(async () => await CreateAccountNewAccount());
        }

        // creating property for create account command
        public ICommand CreateAccountCommand { get; private set; }

        //creating username property
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        //creating password property
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// This method when called verifies the username and password user input from the create account xaml page
        /// </summary>
        /// <returns></returns>
        public async Task CreateAccountNewAccount()
        {   
            //checking if fields are null or empty and showing error
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password field cannot be empty.", "OK");
                return;
            }

            //checks if password is valid and shows error
            Account account = new Account();
            string passwordValidationResult = account.IsValidPassword(Password);
            if (passwordValidationResult == "fail")
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Password", "Ensure password has at least 8 characters, includes both upper and lower case letter, and a number.", "OK");
                return;
            }

            //checking is user can connect to database and display alert
            int connectionSuccessCount = await dbAccess.CheckDatabaseConnection();
            if (connectionSuccessCount == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Connection Error", "Cannot connect to the database. Check your connection and try again.", "OK");
                return;
            }

            //checking if username already exists in the database
            if (await dbAccess.CheckIfUserExists(Username))
            {
                await Application.Current.MainPage.DisplayAlert("User Exists", "The selected username already exists", "OK");
                return;
            }

            //if all correct, creates user account, displays confirmation and navigates back to login page
            await dbAccess.CreateUser(Username, Password);
            await Application.Current.MainPage.DisplayAlert("New User", "Account created successfully. Please take note of your password for later use.", "OK");
            await Shell.Current.GoToAsync("///LoginScreen");
        }

        /// <summary>
        /// This method updates triggers based on event changes
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
