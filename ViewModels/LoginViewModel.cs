using System.Windows.Input;
using Betawave.Classes;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Betawave.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _username;
        private string _password;
        private readonly DatabaseManager _databaseManager;

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            _databaseManager = new DatabaseManager(new DatabaseAccess()); // Ensure you pass correct dependencies
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
        }

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

        public async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Both username and password are required.", "OK");
                return;
            }

            if (await _databaseManager.ValidateUser(Username, Password))
            {
                // Load data from the database
                await _databaseManager.LoadAllDataAsync();

                // Navigation based on user role
                if (await _databaseManager.IsAdmin(Username))
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
