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
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string username;
        private string password;
        private readonly DatabaseManager databaseManager;

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            databaseManager = new DatabaseManager(new DatabaseAccess()); // Ensure you pass correct dependencies
            LoginCommand = new Command(async () => await RunLoginCommand());
        }

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

        public async Task RunLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Both username and password are required.", "OK");
                return;
            }

            if (await databaseManager.ValidateUser(Username, Password))
            {
                // Load data from the database
                await databaseManager.LoadAllDataAsync();

                // Navigation based on user role
                if (await databaseManager.IsAdmin(Username))
                {
                    await Shell.Current.GoToAsync("///AdminDashboard");
                }
                else
                {
                    await Shell.Current.GoToAsync("///MainMenu");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password incorrect.", "OK");
            }


        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
