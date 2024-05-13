/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created to manage the interactions, updating and displaying of the UI on the Login Screen content page  */

using System.Windows.Input;
using Betawave.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Betawave.ViewModels
{   //inheriting from property changed
    public class LoginViewModel : INotifyPropertyChanged
    {   //declaring objects and variables
        public event PropertyChangedEventHandler PropertyChanged;
        private string username;
        private string password;
        private DatabaseAccess dbAccess;
        private DatabaseManager databaseManager;
        //creating command
        public ICommand LoginCommand { get; private set; }

        //class constructor and initialising
        public LoginViewModel()
        {
            dbAccess = new DatabaseAccess();
            databaseManager = new DatabaseManager(dbAccess);
            LoginCommand = new Command(async () => await RunLoginCommand());
        }

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
        /// When called this function checks user input, check there is a valid connection, checks is login information is valid and the user status before logging in
        /// </summary>
        /// <returns></returns>
        public async Task RunLoginCommand()
        {   //input null or whitespace check
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Both username and password are required.", "OK");
                return;
            }
            //connection check
            if (await dbAccess.CheckDatabaseConnection() == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Connection Error", "Cannot connect to the database. Check your connection and try again.", "OK");
                return;
            }
            //user validation check
            if (await dbAccess.ValidateUser(Username, Password))
            {
                // Load data from the database
                 await databaseManager.LoadInAllManagerClassData();

                // Navigation based on user role
                if (dbAccess.IsAdmin(Username))
                {
                    await Shell.Current.GoToAsync("///AdminDashboard");
                }
                else
                {
                    await Shell.Current.GoToAsync("///MainMenu");
                }
            }
            else
            {   //password incorrect error
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password incorrect.", "OK");
            }
        }

        /// <summary>
        /// THis method updates when an event change is triggered
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
