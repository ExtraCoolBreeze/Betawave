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
        private string username;
        private string password;
        private readonly DatabaseManager databaseManager;

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            databaseManager = new DatabaseManager(new DatabaseAccess()); // Ensure you pass correct dependencies
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
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

        public async Task ExecuteLoginCommand()
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
