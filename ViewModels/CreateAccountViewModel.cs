using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Betawave.Classes; // Assuming this namespace contains DatabaseAccess and Account classes.

namespace Betawave.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private readonly DatabaseAccess dbAccess = new DatabaseAccess();

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new Command(async () => await CreateAccountAsync());
        }


        public ICommand CreateAccountCommand { get; private set; }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task CreateAccountAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                return;
            }

            if (await dbAccess.CheckUserExists(Username))
            {
                await Application.Current.MainPage.DisplayAlert("User Exists", "The username is already taken. Please choose a different one.", "OK");
                return;
            }

            await dbAccess.CreateUser(Username, Password);
            await Application.Current.MainPage.DisplayAlert("New User", "Account created successfully. Please take note of your password for later use.", "OK");
            await Shell.Current.GoToAsync("//LoginScreen");
        }


        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
