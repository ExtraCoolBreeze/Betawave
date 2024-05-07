/*Project name: Betawave
Author: Craig McMillan
Date: 06 / 05 / 2024
Project Description: Music player application for HND Software Development Year 2 Graded Unit
Class Description: This view model was created the interactions, updating and displaying of the UI on the CreateAccount content page */

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes; // Assuming this namespace contains DatabaseAccess and Account classes.

namespace Betawave.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;
        private readonly DatabaseAccess dbAccess = new DatabaseAccess();

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new Command(async () => await CreateAccountAsync());
        }


        public ICommand CreateAccountCommand { get; private set; }

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

        public async Task CreateAccountAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username or password field cannot be empty.", "OK");
                return;
            }

            if (await dbAccess.CheckUserExists(Username))
            {
                await Application.Current.MainPage.DisplayAlert("User Exists", "The username is already taken. Please choose a different one.", "OK");
                return;
            }

            await dbAccess.CreateUser(Username, Password);
            await Application.Current.MainPage.DisplayAlert("New User", "Account created successfully. Please take note of your password for later use.", "OK");
            await Shell.Current.GoToAsync("///LoginScreen");
        }


        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
